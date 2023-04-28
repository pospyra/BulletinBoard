using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.UserDto;
using Otiva.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.User
{
    public interface IUserService
    {
        /// <summary>
        /// Добавление DomainUser
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<Guid> RegistrationAsync(RegistrationRequest registration, CancellationToken cancellation);

        /// <summary>
        /// Получить DomainUser по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получить текущего DomainUsera
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public Task<InfoUserResponse> GetCurrentDomainUserAsync(CancellationToken cancellation);

        /// <summary>
        /// Получить всех DomainUser
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(int take, int skip, CancellationToken cancellation);

        /// <summary>
        /// Удалить DomaiUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Редактировать DomainUser
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="update"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<InfoUserResponse> EditUserAsync(Guid Id, UpdateUserRequest update, CancellationToken cancellation);
    }
}
