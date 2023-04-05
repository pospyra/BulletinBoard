using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.UserDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Otiva.AppServeces.Service.User
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        public readonly IUserRepository _userRepository;
        public readonly IIdentityUserService _identityService;
        public readonly IMapper _mapper;
        public UserService(
            IUserRepository userRepository, 
            IMapper mapper,
            IConfiguration configuration,
            IIdentityUserService identityService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _identityService = identityService;
        }

        public async Task DeleteAsync(Guid id)
        {
            var delUser = await _userRepository.FindByIdAsync(id);
            if (delUser == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            await _userRepository.DeleteAsync(delUser);
        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, RegistrationOrUpdateRequest update, byte[] photo )
        {
            var existingAccount = await _userRepository.FindByIdAsync(Id);
            if (existingAccount == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            if (photo != null)
            {
                if (photo.Length > 5242880)
                    throw new Exception("Слишклм большой размер фото");
                existingAccount.KodBase64 = Convert.ToBase64String(photo, 0, photo.Length);
            }

            await _userRepository.EditAdAsync(_mapper.Map(update, existingAccount));

            return _mapper.Map<InfoUserResponse>(update);
        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(int take, int skip)
        {
            return await _userRepository.GetAll()
                .Select(a=> new InfoUserResponse()
                {
                    Id = a.Id,
                    Name= a.UserName,
                    Email= a.Email,
                    Region   = a.Region,
                    Phone= a.PhoneNumber,
                    KodBase64 = a.KodBase64
                }).ToListAsync();
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id)
        {
            var existingUser = await _userRepository.FindByIdAsync(id);
            if (existingUser == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            return _mapper.Map<InfoUserResponse>(existingUser);
        }


        public async Task<Guid> RegistrationAsync(RegistrationOrUpdateRequest registration)
        {
            var existingUser = _userRepository.GetAll().Where(x => x.Email == registration.Email).FirstOrDefault();
            if (existingUser != null)
                throw new Exception("Пльзователь с таким email уже существует");

            var newidentityUserId = await _identityService.RegisterIdentityUser(registration);

            var registerAcc = _mapper.Map<Domain.User.DomainUser>(registration);

            registerAcc.Id = Guid.Parse(newidentityUserId);
            
            await _userRepository.Add(registerAcc);
            return registerAcc.Id;
        }
    }
}
