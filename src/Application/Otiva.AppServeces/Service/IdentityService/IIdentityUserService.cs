using Otiva.Contracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.IdentityService
{
    public interface IIdentityUserService
    {
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<string> Login(LoginRequest userLogin, CancellationToken cancellation);

        /// <summary>
        /// Добавить  IdentityUser
        /// </summary>
        /// <param name="userReg"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<string> RegisterIdentityUser(RegistrationRequest userReg, CancellationToken cancellation);

        public Task EditIdentityUser(string id,UpdateUserRequest userUpdate, CancellationToken cancellation);

        /// <summary>
        /// Получить Id текущего пользователя
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<string> GetCurrentUserIdAsync(CancellationToken cancellation);

        /// <summary>
        /// Удалить IdentityUser
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task DeleteAsync(string Id, CancellationToken cancellation);

        /// <summary>
        /// Получить текущего пользователя
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<InfoIdentityUserResponse> GetCurrentUser(CancellationToken cancellation);

        /// <summary>
        /// Подтвердить пчту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        public Task ConfirmEmail(string userId, string code, CancellationToken cancellationToken);

        /// <summary>
        /// Отправить токен подтверждения на смену почты
        /// </summary>
        /// <param name="newEmail"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task SendTokenOnChangeEmaiAsync(string newEmail, CancellationToken cancellationToken);

        /// <summary>
        /// Подтверждение смены почты пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="newEmail">новая почта пользователя</param>
        /// <param name="token">токен подтверждения</param>
        /// <param name="cancellationToken"></param>
        public Task<string> ConfirmChangeEmail(string userId, string newEmail, string token, CancellationToken cancellationToken);

        /// <summary>
        /// Поменять пароль
        /// </summary>
        /// <param name="changePassword"></param>
        /// <param name="cancellationToken"></param>
        public Task ChangePasswordAsync(ChangePassword changePassword, CancellationToken cancellationToken);

        /// <summary>
        /// Получить Id пользователей, которые не подтвердили свою почту
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<Guid>> GetNotConfirmAccount();
    }
}
