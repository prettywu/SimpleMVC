
using System.Web.Mvc;

namespace SimpleMVC.Common
{
    public class Json : JsonResult
    {
        public bool isSuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public Json()
        {
            isSuccess = false;
        }

        public Json(JsonRequestBehavior behavior)
        {
            JsonRequestBehavior = behavior;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                isSuccess = this.isSuccess,
                code = this.code,
                message = this.message,
                data = data
            };
            base.ExecuteResult(context);
        }
    }

    public class JsonPage : JsonResult
    {
        public bool isSuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public int _pageindex { get; set; }
        public int _pageSize { get; set; }
        public int _total { get; set; }

        public JsonPage(int pageIndex,int pageSize,int total,JsonRequestBehavior behavior)
        {
            _pageindex = pageIndex;
            _pageSize = pageSize;
            _total = total;
            JsonRequestBehavior = behavior;
            isSuccess = true;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                isSuccess = this.isSuccess,
                code = this.code,
                message = this.message,
                data = new
                {
                    pageIndex = _pageindex,
                    pageSize = _pageSize,
                    total = _total,
                    list=data
                }
            };
            base.ExecuteResult(context);
        }
    }
}