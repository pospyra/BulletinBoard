using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Otiva.AppServeces.IRepository;
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
        private readonly IClaimAccessor _claimAccessor;
        public readonly IMapper _mapper;
        public UserService(
            IUserRepository userRepository, 
            IMapper mapper,
            IConfiguration configuration, 
            IClaimAccessor claimAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _claimAccessor = claimAccessor;
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
                    Name= a.Name,
                    Email= a.Email,
                    Region   = a.Region,
                    Phone= a.Phone,
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

        public async Task<InfoUserResponse> GetCurrent(CancellationToken cancellation)
        {
            var claim = await _claimAccessor.GetClaims(cancellation);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
                return null;

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindByIdAsync(id);

            if (user == null)
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");

            return _mapper.Map<InfoUserResponse>(user);
        }

        public async Task<Guid> GetCurrentUserId(CancellationToken cancellation)
        {
            var claim = await _claimAccessor.GetClaims(cancellation);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
                throw new Exception("Не найдент пользователь с идентификаторром");

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindByIdAsync(id);

            if (user == null)
                throw new Exception($"Не найдент пользователь с идентификаторром {id}");

            return user.Id;
        }

        public async Task<string> Login(LoginRequest userLogin)
        {
            var existingUser = await _userRepository.FindWhere(user => user.Email == userLogin.Email); 

            if (existingUser == null)
                throw new Exception($"Пользователь с email '{userLogin.Email}' не существует");

            if (!existingUser.Password.Equals(userLogin.Password))
                throw new Exception($"Указан неверный логин или пароль");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, existingUser.Email)
            };

            var secretKey = _configuration["Token:SecretKey"];

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                    )
                );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            return result;
        }

        public async Task<Guid> RegistrationAsync(RegistrationOrUpdateRequest registration, byte[] photo)
        {
            var existingUser = _userRepository.GetAll().Where(x => x.Email == registration.Email).FirstOrDefault();
            if (existingUser != null)
                throw new Exception("Такой пользователь уже существует");

            var registerAcc = _mapper.Map<Domain.User>(registration);

            if(photo != null)
            {
                if (photo.Length > 5242880)
                    throw new Exception("Слишклм большой размер фото");
                registerAcc.KodBase64 = Convert.ToBase64String(photo, 0, photo.Length);
            }         

            await _userRepository.Add(registerAcc);
            return registerAcc.Id;
        }
    }
}
