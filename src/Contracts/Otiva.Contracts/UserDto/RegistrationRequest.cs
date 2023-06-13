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
    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    public class RegistrationRequest
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [CheckCurseWord]
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [ValidatePassword]
        public string Password { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Номет телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateBirthday { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        /// Идентификаторы фотографий пользователя
        /// </summary>
        public ICollection<Guid>? PhotoId { get; set; }
    }
}
