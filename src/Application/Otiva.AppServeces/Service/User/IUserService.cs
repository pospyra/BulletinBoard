using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.User
{
    public interface IUserService
    {
        Task<InfoUserResponse> GetByIdAsync(Guid id);

        Task<Guid> RegistrationAsync(RegistrationOrUpdateRequest registration);

        Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(int take, int skip);

        Task DeleteAsync(Guid id);

        Task<InfoUserResponse> EditUserAsync(Guid Id, RegistrationOrUpdateRequest registration);
    }
}
