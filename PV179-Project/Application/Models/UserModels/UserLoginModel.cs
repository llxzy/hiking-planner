using System.ComponentModel.DataAnnotations;

namespace Application.Models.UserModels
{
    public class UserLoginModel
    {
        [Required]
        public string MailAddress { get; set; }
        [Required]
        public string Password    { get; set; }
        
    }
}