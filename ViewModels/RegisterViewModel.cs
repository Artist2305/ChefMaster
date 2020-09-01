using Dinner.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dinner.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 50 characters")]
        public string AccountName { get; set; }
        [Required]
        [EmailAddress]
        [Remote(action:"IsEmailInUse", controller:"Account")]
        [ValidEmailDomain(allowedDomain: "gmail.com", 
            ErrorMessage = "Email domain must be gmail.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public IFormFile AvatarPhoto { get; set; }

    }
}
