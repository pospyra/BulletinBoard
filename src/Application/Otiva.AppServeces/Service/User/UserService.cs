using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Org.BouncyCastle.Asn1.Ocsp;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.Photo;
using Otiva.Contracts.CategoryDto;
using Otiva.Contracts.UserDto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Otiva.AppServeces.Service.User
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityUserService _identityService;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository, 
            IMapper mapper,
            IConfiguration configuration,
            IIdentityUserService identityService,
            ILogger<UserService> logger
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            await _identityService.DeleteAsync(id.ToString(), cancellation);

            var delUser = await _userRepository.FindByIdAsync(id, cancellation);
            if (delUser == null)
               throw new Exception("Пользователь с данным идентификатором не найден");

            await _userRepository.DeleteAsync(delUser, cancellation);

        }

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, RegistrationOrUpdateRequest update, byte[] photo, CancellationToken cancellation)
        {
            var existingAccount = await _userRepository.FindByIdAsync(Id, cancellation);
            if (existingAccount == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            if (photo != null)
            {
                if (photo.Length > 5242880)
                    throw new Exception("Слишклм большой размер фото");
                existingAccount.KodBase64 = Convert.ToBase64String(photo, 0, photo.Length);
            }

            await _userRepository.EditAdAsync(_mapper.Map(update, existingAccount), cancellation);

            return _mapper.Map<InfoUserResponse>(update);
        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(int take, int skip, CancellationToken cancellation)
        {
            return await _userRepository.GetAll(cancellation)
                .Select(a=> new InfoUserResponse()
                {
                    Id = a.Id,
                    UserName= a.UserName,
                    Email= a.Email,
                    Region   = a.Region,
                    PhoneNumber = a.PhoneNumber,
                    KodBase64 = a.KodBase64
                }).ToListAsync();
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingUser = await _userRepository.FindByIdAsync(id, cancellation);
            if (existingUser == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            return _mapper.Map<InfoUserResponse>(existingUser);
        }


        public async Task<Guid> RegistrationAsync(RegistrationOrUpdateRequest registration, CancellationToken cancellation)
        {
           var existingUser = _userRepository.GetAll(cancellation)
           .Where(x => x.Email == registration.Email).FirstOrDefault();

            if (existingUser != null)
                throw new Exception("Пльзователь с таким email уже существует");

            var newidentityUserId = await _identityService.RegisterIdentityUser(registration, cancellation);

            var registerAcc = _mapper.Map<Domain.User.DomainUser>(registration);

            registerAcc.Id = Guid.Parse(newidentityUserId);
            registerAcc.DateBirthday = registration.DateBirthday.ToUniversalTime();
            await _userRepository.Add(registerAcc, cancellation);

            if(registration.PhotoId!= null)
            {
                foreach (var photoId in registration.PhotoId)
                {
                    await _photoService.SetPhotoUserAsync(photoId, registerAcc.Id, cancellation);
                }
            }
            return registerAcc.Id;
        }
    }
}
