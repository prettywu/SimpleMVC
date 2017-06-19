using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Common
{
    public static class LambdaExtention
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr2 != null)
            {
                if (expr1 == null)
                    expr1 = expr2;
                else
                    expr1 = Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, expr2.Body), expr1.Parameters);
            }

            return expr1;
        }
        //public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        //{
        //    if (expr2 != null)
        //    {
        //        if (expr1 == null)
        //            expr1 = expr2;
        //        else
        //            expr1 = Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, expr2.Body), expr1.Parameters);
        //    }
        //    return expr1;
        //}


        //public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        //{
        //    var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
        //    return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);
        //}

        //public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        //{
        //    var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
        //    return Expression.Lambda<Func<T, bool>>(Expression.And(expression1.Body, invokedExpression), expression1.Parameters);
        //}
    }

    public class LamadaExtention<T> where T : new()
    {
        private List<Tuple<Expression, RelationType>> list_expression = null;
        private ParameterExpression m_parameter = null;

        public LamadaExtention()
        {
            list_expression = new List<Tuple<Expression, RelationType>>();
            m_parameter = Expression.Parameter(typeof(T), "u");
        }

        public void AddExpression(Expression<Func<T, bool>> exp, RelationType type = RelationType.And)
        {
            list_expression.Add(new Tuple<Expression, RelationType>(exp, type));

        }

        //构造表达式，存放到list_expression集合里面
        public void AddExpression(string strPropertyName, object strValue, ConditionType expressType, RelationType type = RelationType.And)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_parameter, strPropertyName);
            if (expressType == ConditionType.Contains)
            {
                expRes = Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue));
            }
            else if (expressType == ConditionType.Equal)
            {
                expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ConditionType.LessThan)
            {
                expRes = Expression.LessThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ConditionType.LessThanOrEqual)
            {
                expRes = Expression.LessThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ConditionType.GreaterThan)
            {
                expRes = Expression.GreaterThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ConditionType.GreaterThanOrEqual)
            {
                expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }

            list_expression.Add(new Tuple<Expression, RelationType>(expRes, type));
        }

        //针对Or条件的表达式
        //public void ExpressionOr(string strPropertyName, List<object> lstValue)
        //{
        //    Expression expRes = null;
        //    MemberExpression member = Expression.PropertyOrField(m_parameter, strPropertyName);
        //    foreach (var oValue in lstValue)
        //    {
        //        if (expRes == null)
        //        {
        //            expRes = Expression.Equal(member, Expression.Constant(oValue, member.Type));
        //        }
        //        else
        //        {
        //            expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(oValue, member.Type)));
        //        }
        //    }


        //    list_expression.Add(expRes);
        //}

        //得到Lamada表达式的Expression对象
        public Expression<Func<T, bool>> GetLambda()
        {
            Expression whereExpr = null;
            foreach (var expr in this.list_expression)
            {
                if (whereExpr == null) whereExpr = expr.Item1;
                else if (expr.Item2 == RelationType.And)
                    whereExpr = Expression.And(whereExpr, expr.Item1);
                else
                    whereExpr = Expression.Or(whereExpr, expr.Item1);
            }
            if (whereExpr == null)
                return null;
            return Expression.Lambda<Func<T, Boolean>>(whereExpr, m_parameter);
        }
    }

    //用于区分操作的枚举
    public enum ConditionType
    {
        Contains,//like
        Equal,//等于
        LessThan,//小于
        LessThanOrEqual,//小于等于
        GreaterThan,//大于
        GreaterThanOrEqual//大于等于
    }

    public enum RelationType
    {
        And = 0,
        Or = 1
    }
}
