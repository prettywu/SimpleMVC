﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVC.ViewModels
{
    public class AddUserViewModel
    {
        public string username { get; set; }

        public string email { get; set; }

        public string phone { get; set; }
        
        public string nickname { get; set; }

        public string headimage { get; set; }

        public int gender { get; set; }

        public string birthday { get; set; }

        public string location { get; set; }
    }
}