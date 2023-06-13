using Otiva.Contracts.Attributs;

namespace Otiva.Contracts.UserDto
{
    /// <summary>
    /// Смена пароля
    /// </summary>
    public class ChangePassword
    {
        /// <summary>
        /// Текущий пароль
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        [ValidatePassword]
        public string NewPassword { get; set; }
    }
}
