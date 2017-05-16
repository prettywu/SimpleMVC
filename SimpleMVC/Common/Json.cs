
using System.Web.Mvc;

namespace SimpleMVC.Common
{
    public class Json : JsonResult
    {
        public bool isSuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }

        public Json() : base() { }

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
                message = this.message
            };
            base.ExecuteResult(context);
        }
    }
}