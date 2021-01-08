using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.UserModels
{
    public class UserCreateModel
    {
        [Required]
        [StringLength(40, ErrorMessage = "Maximum length for username is 40 characters")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(50, ErrorMessage = "Maximum length for email is 50 characters")]
        public string MailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
