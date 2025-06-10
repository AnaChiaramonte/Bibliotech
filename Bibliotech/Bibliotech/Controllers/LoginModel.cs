﻿using System.ComponentModel.DataAnnotations;

namespace Bibliotech.Controllers
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Lembrar-me")]
        public bool? RememberMe { get; set; } = false;



    }
}