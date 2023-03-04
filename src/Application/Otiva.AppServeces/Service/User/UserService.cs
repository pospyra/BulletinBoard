using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Otiva.AppServeces.IRepository;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Otiva.AppServeces.Service.User
{
    public class UserService : IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task DeleteAsync(Guid id)
        {
            var delUser = await _userRepository.FindByIdAsync(id);
            if (delUser == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            await _userRepository.DeleteAsync(delUser);

        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, RegistrationOrUpdateRequest update )
        {
            var existingAccount = await _userRepository.FindByIdAsync(Id);
            if (existingAccount == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            await _userRepository.EditAdAsync(_mapper.Map(update, existingAccount));

            return _mapper.Map<InfoUserResponse>(update);
        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(int take, int skip)
        {
            return await _userRepository.GetAll()
                .Select(a=> new InfoUserResponse()
                {
                    Id = a.Id,
                    Name= a.Name,
                    Email= a.Email,
                    Password= a.Password,
                    Region   = a.Region,
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
            var registerAcc = _mapper.Map<Domain.User>(registration);
            var existingUser = _userRepository.GetAll().Where(x => x.Email == registration.Email).FirstOrDefault();
            if (existingUser != null)
                throw new Exception("Такой пользователь уже существует");

            await _userRepository.Add(registerAcc);
            return registerAcc.Id;
        }
    }
}
