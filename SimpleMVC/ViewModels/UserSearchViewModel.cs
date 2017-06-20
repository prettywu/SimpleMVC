using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.ViewModels
{
    public class UserSearchViewModel
    {
        public UserSearchViewModel()
        {
            sortname = "RegistTime";
            role = -1;
            state = -1;
            gender = -1;
            page = 1;
            pagesize = 10;
        }
        //过滤
        public string username { get; set; }
        public string email { get; set; }
        public string nickname { get; set; }
        public string birthday { get; set; }
        public string phone { get; set; }
        public int gender { get; set; }
        public int role { get; set; }
        public int state { get; set; }
        public string registtimerange { get; set; }
        //排序
        public string sortname { get; set; }
        public int sorttype { get; set; }
        //分页
        public int page { get; set; }
        public int pagesize { get; set; }

        public DateTime? Birthdate
        {
            get {
                if (!string.IsNullOrEmpty(birthday))
                    return Convert.ToDateTime(birthday);
                else
                    return null;        
            }
        }

        public DateTime? StartDate
        {
            get {
                if (!string.IsNullOrEmpty(registtimerange) && registtimerange.Contains("-"))
                    return Convert.ToDateTime(registtimerange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                else
                    return null;
            }
        }

        public DateTime? EndDate
        {
            get {
                if (!string.IsNullOrEmpty(registtimerange) && registtimerange.Contains("-"))
                    return Convert.ToDateTime(registtimerange.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[1]);
                else
                    return null;
            }
        }
    }
}