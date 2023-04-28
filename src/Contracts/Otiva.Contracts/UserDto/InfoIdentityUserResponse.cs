using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.UserDto
{
    public class InfoIdentityUserResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateRegistration { get; set; }

        public bool EmailConfirmed { get; set; }

        public IList<string> Role { get; set; }
    }
}
