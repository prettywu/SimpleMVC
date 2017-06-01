using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.ViewModels
{
    public class UserSearchViewModel
    {
        public string username { get; set; }
        public string nickname { get; set; }
        public string sortname { get; set; }
        public int sorttype { get; set; }
        public int role { get; set; }
        public int state { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
    }
}