using System.ComponentModel.DataAnnotations;

namespace Application.Models.UserModels
{
    public class UserChangePasswordModel : BaseModel
    {
        [Required]
        public string Password        { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
