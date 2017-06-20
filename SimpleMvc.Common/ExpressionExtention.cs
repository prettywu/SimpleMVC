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
                {
                    var parameter = Expression.Parameter(typeof(T));
                    var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
                    var left = leftVisitor.Visit(expr1.Body);

                    var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
                    var right = rightVisitor.Visit(expr2.Body);

                    expr1= Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);

                    //expr1 = Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, expr2.Body),expr1.Parameters);
                    //ParameterExpression param = Expression.Parameter(typeof(T));
                    //expr1 = Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, expr2.Body), param);
                }
                   
            }

            return expr1;
        }


        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            if (expr2 != null)
            {
                if (expr1 == null)
                    expr1 = expr2;
                else
                {
                    var parameter = Expression.Parameter(typeof(T));
                    var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
                    var left = leftVisitor.Visit(expr1.Body);

                    var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
                    var right = rightVisitor.Visit(expr2.Body);

                    return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
                }
                   
            }
            return expr1;
        }
    }

    class ReplaceExpressionVisitor: ExpressionVisitor
    {
        private readonly Expression _oldValue;
        private readonly Expression _newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            _oldValue = oldValue;
            _newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == _oldValue)
                return _newValue;
            return base.Visit(node);
        }
    }
}
