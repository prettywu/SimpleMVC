﻿
using System.ComponentModel.DataAnnotations;

namespace SimpleMVC.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我")]
        public bool RememberMe { get; set; }
    }
}