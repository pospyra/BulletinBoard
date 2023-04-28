using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.UserDto
{
    public class InfoUserResponse
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Region { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public DateTime DateBirthday { get; set; }
    }
}
