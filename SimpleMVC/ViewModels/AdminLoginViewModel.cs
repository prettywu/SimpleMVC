﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleMVC.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}