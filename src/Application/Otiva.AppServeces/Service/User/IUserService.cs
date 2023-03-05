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
        Task<Guid> GetCurrentUserId(CancellationToken cancellation);
        Task<string> Login(LoginRequest userLogin);
        Task<InfoUserResponse> GetCurrent(CancellationToken cancellation);
        Task<Guid> RegistrationAsync(RegistrationOrUpdateRequest registration, byte[] photo);

        Task<InfoUserResponse> GetByIdAsync(Guid id);

        Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(int take, int skip);

        Task DeleteAsync(Guid id);

        Task<InfoUserResponse> EditUserAsync(Guid Id, RegistrationOrUpdateRequest update, byte[] photo);
    }
}
