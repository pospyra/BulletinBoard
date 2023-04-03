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
        //убрать
        public Task<Guid> GetCurrentUserId(CancellationToken cancellation);

        /// <summary>
        /// Добавление DomainUser
        /// </summary>
        /// <param name="registration"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<Guid> RegistrationAsync(RegistrationOrUpdateRequest registration, byte[] photo);

        /// <summary>
        /// Получить DomainUser по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InfoUserResponse> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить всех DomainUser
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(int take, int skip);

        /// <summary>
        /// Удалить DomaiUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Редактировать DomainUser
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="update"></param>
        /// <param name="photo"></param>
        /// <returns></returns>
        Task<InfoUserResponse> EditUserAsync(Guid Id, RegistrationOrUpdateRequest update, byte[] photo);
    }
}
