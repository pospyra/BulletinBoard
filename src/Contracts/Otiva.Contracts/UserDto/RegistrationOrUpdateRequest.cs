using Microsoft.AspNetCore.Http;
using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.UserDto
{
    public class RegistrationOrUpdateRequest
    {
        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [ValidatePassword]
        public string Password { get; set; }

        public string Region { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateBirthday { get; set; }

        public string? Role { get; set; }
    }
}
