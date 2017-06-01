﻿
using System.Web.Mvc;

namespace SimpleMvc.Common
{
    public class Json : JsonResult
    {
        public bool isSuccess { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public Json(string errormessage)
        {
            isSuccess = false;
            message = errormessage;
            code = 500;
        }

        public Json(bool success, int code, string message, object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            isSuccess = success;
            this.code = code;
            this.message = message;
            this.data = data;
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
        private object _data { get; set; }
        private int _pageindex { get; set; }
        private int _pageSize { get; set; }
        private int _total { get; set; }

        public JsonPage(int pageIndex, int pageSize, int total, object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
        {
            _pageindex = pageIndex;
            _pageSize = pageSize;
            _total = total;
            _data = data;
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
                    list = _data
                }
            };
            base.ExecuteResult(context);
        }
    }
}