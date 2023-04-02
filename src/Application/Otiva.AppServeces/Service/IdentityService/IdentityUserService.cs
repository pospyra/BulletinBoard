using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Otiva.AppServeces.IRepository;
using Otiva.AppServeces.Service.User;
using Otiva.Contracts.UserDto;
using Otiva.Domain.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.IdentityService
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<Domain.User.IdentityUser> _userManager;
        private readonly IClaimAccessor _claimAccessor;
        private readonly IConfiguration _configuration;


        public IdentityUserService(
            UserManager<Domain.User.IdentityUser> userManager,
            IClaimAccessor claimAccessor, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _claimAccessor = claimAccessor;
            _configuration = configuration;
        }

        //public async Task<InfoUserResponse> GetCurrent(CancellationToken cancellation)
        //{
        //    var claim = await _claimAccessor.GetClaims(cancellation);
        //    var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrWhiteSpace(claimId))
        //        return null;

        //    var id = Guid.Parse(claimId);
        //    var user = await _userRepository.FindByIdAsync(id);

        //    if (user == null)
        //        throw new Exception($"Не найдент пользователь с идентификаторром {id}");

        //    return _mapper.Map<InfoUserResponse>(user);
        //}

        //public async Task<Guid> GetCurrentUserId(CancellationToken cancellation)
        //{
        //    var claim = await _claimAccessor.GetClaims(cancellation);
        //    var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        //    if (string.IsNullOrWhiteSpace(claimId))
        //        throw new Exception("Не найдент пользователь с идентификаторром");

        //    var id = Guid.Parse(claimId);
        //    var user = await _userRepository.FindByIdAsync(id);

        //    if (user == null)
        //        throw new Exception($"Не найдент пользователь с идентификаторром {id}");

        //    return user.Id;
        //}

        public async Task<string> Login(LoginRequest userLogin)
        {

            var existingUser = await _userManager.FindByEmailAsync(userLogin.Email);
            if (existingUser == null)
                throw new Exception($"Пользователь с email '{userLogin.Email}' не существует");

            var checkPass = await _userManager.CheckPasswordAsync(existingUser, userLogin.Password);
            if(!checkPass)
                throw new Exception("Неверная почта или пароль");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, existingUser.UserName)
            };

            //var userRole = await _userManager.GetRolesAsync(existingUser);
            //claims.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role)));


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

        public async Task<string> RegisterIdentityUser(RegistrationOrUpdateRequest userReg)
        {
            var newIdentityUser = new Domain.User.IdentityUser
            {
                UserName = userReg.Name,
                Email = userReg.Email,
                PasswordHash = userReg.Password,
                PhoneNumber = userReg.Phone
            };

            var resRegister = await _userManager.CreateAsync(newIdentityUser, userReg.Password);

            if (resRegister.Succeeded)
                await _userManager.AddToRoleAsync(newIdentityUser, userReg.Role);

            return newIdentityUser.Id;
        }
    }

}
