using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.Contracts.UserDto
{
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public class InfoIdentityUserResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime DateRegistration { get; set; }

        /// <summary>
        /// Подтверждение почты
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Роли пользователя 
        /// </summary>
        public IList<string> Role { get; set; }
    }
}
