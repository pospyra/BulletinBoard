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
    /// <summary>
    /// Логин
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
