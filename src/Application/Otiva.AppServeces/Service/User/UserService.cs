using AutoMapper;
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
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, RegistrationOrUpdateRequest update )
        {
            var existingAccount = await _userRepository.FindById(Id);

            await _userRepository.EditAdAsync(_mapper.Map(update, existingAccount));

            return _mapper.Map<InfoUserResponse>(update);
        }

        public Task<IReadOnlyCollection<InfoUserResponse>> GetAll(int take, int skip)
        {
            throw new NotImplementedException();
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id)
        {
            var existingUser = await _userRepository.FindById(id);
            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<Guid> Registration(RegistrationOrUpdateRequest registration)
        {
            var registerAcc = _mapper.Map<Domain.User>(registration);
            var existingUser = _userRepository.GetAll().Where(x => x.Email == registration.Email);
            if(existingUser != null)
                throw new Exception("такой пользователь уже существует");
           
            await _userRepository.AddAsync(registerAcc);
            return registerAcc.Id;
        }
    }
}
