using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Models.UserModels
{
    public class UserCreateModel
    {
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
    }
}
