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
        public Task<string> RegisterIdentityUser(RegistrationOrUpdateRequest userReg, CancellationToken cancellation);

        /// <summary>
        /// Получить Id текущего пользователя
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<string> GetCurrentUserId(CancellationToken cancellation);

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
        public Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellation);

        /// <summary>
        /// Подтвердить пчту
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ConfirmEmail(string userId, string code, CancellationToken cancellationToken);
    }
}
