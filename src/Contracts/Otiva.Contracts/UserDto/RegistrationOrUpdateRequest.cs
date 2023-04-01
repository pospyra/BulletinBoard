using Microsoft.AspNetCore.Http;
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
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="Пароль должен содержать не менее 6ти символов")]
        public string Password { get; set; }

        public string Region { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }
    }
}
