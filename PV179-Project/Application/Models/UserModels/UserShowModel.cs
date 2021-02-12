using Application.Models;

namespace API.Models
{
    public class UserShowModel : BaseModel
    {
        public string Name        { get; set; }
        public string MailAddress { get; set; }
    }
}
