using Otiva.Contracts.Attributs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.UserDto
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
      //  [MinLength(6, ErrorMessage ="Пароль должен содержать не менее 6ти символов")]
        public string Password { get; set; }
    }
}
