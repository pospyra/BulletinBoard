using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.Photo;
using Otiva.Contracts.UserDto;

namespace Otiva.AppServeces.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityUserService _identityService;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly ILogger<UserService> _logger;

        public UserService
            (IUserRepository userRepository, 
            IMapper mapper,
            IIdentityUserService identityService,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        public async Task<InfoUserResponse> EditUserAsync(Guid Id, UpdateUserRequest update, CancellationToken cancellation)
        {
            var existingAccount = await _userRepository.FindByIdAsync(Id, cancellation);
            if (existingAccount == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            if (update.PhotoId != null)
            {
                foreach (var photoId in update.PhotoId)
                {
                    await _photoService.SetPhotoUserAsync(photoId, existingAccount.Id, cancellation);
                }
            }
            await _userRepository.EditUserAsync(_mapper.Map(update, existingAccount), cancellation);
            await _identityService.EditIdentityUser(Id.ToString(), update, cancellation);

            return _mapper.Map<InfoUserResponse>(existingAccount);
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
                    PhoneNumber = a.PhoneNumber
                }).ToListAsync();
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            var existingUser = await _userRepository.FindByIdAsync(id, cancellation);
            if (existingUser == null)
                throw new Exception("Пользователь с таким идентификатором не найден");

            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<InfoUserResponse> GetCurrentDomainUserAsync( CancellationToken cancellation)
        {
            var existingUserId = await _identityService.GetCurrentUserIdAsync(cancellation);
            var existingUser = await _userRepository.FindByIdAsync(Guid.Parse(existingUserId), cancellation);
            return _mapper.Map<InfoUserResponse>(existingUser);
        }

        public async Task<Guid> RegistrationAsync(RegistrationRequest registration, CancellationToken cancellation)
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
