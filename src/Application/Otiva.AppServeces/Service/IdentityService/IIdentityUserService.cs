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
        public Task<string> Login(LoginRequest userLogin);
        public Task<string> RegisterIdentityUser(RegistrationOrUpdateRequest userReg);

    }
}
