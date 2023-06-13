using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Otiva.Contracts.UserDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace Otiva.AppServeces.Service.IdentityService
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<Domain.User.IdentityUser> _userManager;
        private readonly IClaimAccessor _claimAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<IdentityUserService> _logger;

        public IdentityUserService
            (UserManager<Domain.User.IdentityUser> userManager,
            IClaimAccessor claimAccessor,
            IConfiguration configuration,
            IMapper mapper,
            IMemoryCache memoryCache,
            ILogger<IdentityUserService> logger)
        {
            _userManager = userManager;
            _claimAccessor = claimAccessor;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task DeleteAsync(string Id, CancellationToken cancellation)
        {
            _logger.LogInformation("Удаление IdentityUser из базы данных");

            var identityUSer = await _userManager.FindByIdAsync(Id);
            if (identityUSer == null)
                throw new Exception("Пользователь с данным идентификатором не найден");

            await _userManager.DeleteAsync(identityUSer);
        }

        public async Task EditIdentityUser(string id, UpdateUserRequest userUpdate, CancellationToken cancellation)
        {
            var existingUser = await _userManager.FindByIdAsync(id);

            var resUpdating = await _userManager.UpdateAsync(_mapper.Map(userUpdate, existingUser));

            if (!resUpdating.Succeeded)
                throw new Exception("Данные IdentityUser не удалось обновить");
        }

        public async Task<string> RegisterIdentityUser(RegistrationRequest userReg, CancellationToken cancellation)
        {
            _logger.LogInformation("Регистрация пользователя в системе");

            var userNameCheck = await _userManager.FindByNameAsync(userReg.UserName);
            if (userNameCheck != null)
                throw new Exception("Пользователь с таким именем уже существует");

            var newIdentityUser = new Domain.User.IdentityUser
            {
                UserName = userReg.UserName,
                Email = userReg.Email,
                PasswordHash = userReg.Password,
                PhoneNumber = userReg.PhoneNumber
            };

            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            var resRegister = await _userManager.CreateAsync(newIdentityUser, userReg.Password);

            if (resRegister.Succeeded && userReg.Role != null)
                await _userManager.AddToRoleAsync(newIdentityUser, userReg.Role);

            else if (resRegister.Succeeded && userReg.Role == null)
                await _userManager.AddToRoleAsync(newIdentityUser, "User");

            else
            {
                _logger.LogWarning("Не удалось зарегистрировать IdentityUser");
                throw new Exception("Не удалось зарегистрировать пользователя");
            }
            await SendConfirmMail(newIdentityUser.Id, cancellation);

            _logger.LogInformation("Пользователь успешно зарегистрирвоался в системе");
            return newIdentityUser.Id;
        }

        public async Task<string> Login(LoginRequest userLogin, CancellationToken cancellation)
        {
            _logger.LogInformation("Аутентификация пользователя в системе");

            var existingUser = await _userManager.FindByEmailAsync(userLogin.Email);

            if (existingUser == null)
                throw new Exception($"Пользователь с email '{userLogin.Email}' не существует");

            var checkPass = await _userManager.CheckPasswordAsync(existingUser, userLogin.Password);
            if (!checkPass)
                throw new Exception("Неверная почта или пароль");

            var IsEmailConfirm = await _userManager.IsEmailConfirmedAsync(existingUser);
            if (!IsEmailConfirm)
                throw new Exception("Почта не подтверждена");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, existingUser.UserName)
            };
            var userRole = await _userManager.GetRolesAsync(existingUser);
            claims.AddRange(userRole.Select(role => new Claim(ClaimTypes.Role, role)));

            var secretKey = _configuration["Token:SecretKey"];

            var token = new JwtSecurityToken
                (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256)
               );

            var result = new JwtSecurityTokenHandler().WriteToken(token);

            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            _logger.LogInformation("Аутентификация прошла успешно");

            return result;
        }

        public async Task<InfoIdentityUserResponse> GetCurrentUser(CancellationToken cancellation)
        {
            _logger.LogInformation("Получение текущего пользователя");

            var claim = await _claimAccessor.GetClaims(cancellation);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
                throw new Exception("Пользователь не авторизован");

            var user = await _userManager.FindByIdAsync(claimId);

            if (user == null)
                throw new Exception($"Не найдент пользователь с идентификаторром {claimId}");

            var userResponse = _mapper.Map<InfoIdentityUserResponse>(user);
            userResponse.Role = await _userManager.GetRolesAsync(user);

            return userResponse;
        }

        public async Task<string> GetCurrentUserIdAsync(CancellationToken cancellation)
        {
            var cacheKey = "CurrentUserId";
            var userId = _memoryCache.Get<string>(cacheKey);

            if (userId != null)
            {
                _logger.LogInformation("Идентификатор текущего пользователя взят из кэша");
                return userId;
            }

            _logger.LogInformation("Получение Id текущего пользователя");

            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            var claim = await _claimAccessor.GetClaims(cancellation);
            var claimId = claim.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
                throw new Exception("Пользователь не найден");

            var user = await _userManager.FindByIdAsync(claimId);

            if (user == null)
                throw new Exception($"Не найдент пользователь с идентификаторром {claimId}");

            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };

            _memoryCache.Set(cacheKey, user.Id, options); 
            return user.Id;
        }

        public async Task SendConfirmMail(string userId, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Отправка кода подтверждения на почту пользователя");
                var identityUser = await _userManager.FindByIdAsync(userId);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                
                var callbackUrl = $"https://localhost:7278/confirmEmail?userId={identityUser.Id}&code={HttpUtility.UrlEncode(code)}";

                _logger.LogInformation("Отправка кода подтверждения на почту пользователя");

                EmailService.EmailService emailService = new EmailService.EmailService();
                await emailService.SendEmailAsync(identityUser.Email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>" +
                    $"Если вы не подтвердите почту в течении двух дней, Ваш аккаунт будет удален");

                _logger.LogInformation("Письмо с кодом подтверждения отправлено на почту пользователя");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task ConfirmEmail(string userId, string code, CancellationToken cancellationToken)
        {
            if (userId == null || code == null)
                throw new ArgumentException("Поля не могут быть пустыми");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("Не найден пользователь");

            if (user.EmailConfirmed)
                throw new Exception("Ваша почта уже подтверждена");

            var result = await _userManager.ConfirmEmailAsync(user, code);
           
            if (!result.Succeeded)
                throw new Exception("Ошибка подтверждения");

            _logger.LogInformation($"Пользователь с id {userId} подтвердил свою почту {DateTime.UtcNow}");
        }

        public async Task ChangePasswordAsync(ChangePassword changePassword, CancellationToken cancellationToken)
        {
            var currntUserId = await GetCurrentUserIdAsync(cancellationToken);
            var currntUser = await _userManager.FindByIdAsync(currntUserId);

            var result = await _userManager.ChangePasswordAsync(currntUser, changePassword.CurrentPassword, changePassword.NewPassword);
            if (!result.Succeeded)
            {
                _logger.LogInformation("Попытка изменения пароля");
                throw new ArgumentException("Не удалось изменить пароль. Повторите попытку");
            }
        }

        public async Task SendTokenOnChangeEmaiAsync(string newEmail, CancellationToken cancellationToken)
        {
            var currntUserId = await GetCurrentUserIdAsync(cancellationToken);
            var currntUser = await _userManager.FindByIdAsync(currntUserId);

            _logger.LogInformation("Отправка токена подтверждения на смену почты пользователя");
            var token = await _userManager.GenerateChangeEmailTokenAsync(currntUser, newEmail);
            var callbackUrl = $"https://localhost:7278/confirmChangeEmail?userId={currntUser.Id}&newEmail={newEmail}&token={HttpUtility.UrlEncode(token)}";

            EmailService.EmailService emailService = new EmailService.EmailService();
            await emailService.SendEmailAsync(newEmail, "Confirm your new Email",
                $"Подтвердите смену почты, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

            _logger.LogInformation("Письмо было отправлено пользователю");
        }

        public async Task<string> ConfirmChangeEmail(string userId, string newEmail, string token, CancellationToken cancellationToken)
        {
            if (userId == null || token == null)
                throw new ArgumentException("Поля не могут быть пустыми");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("Не найден пользователь");

            if (user.EmailConfirmed)
                throw new Exception("Вы уже сменили почту");

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (!result.Succeeded)
                throw new Exception("Ошибка подтверждения");

            _logger.LogInformation($"Пользователь с id {userId} подтвердил смену почту {DateTime.UtcNow}");
            return "Ваш Email был успешно изменен";
        }

        public async Task<ICollection<Guid>> GetNotConfirmAccount()
        {
            //TODO переписать под ProjectTo. настроить конфигурацию мапера
            return await _userManager.Users
             .Where(u => !u.EmailConfirmed && u.DateRegistration < DateTime.UtcNow.AddDays(-1)) 
             .Select(u => Guid.Parse(u.Id))
             .ToListAsync();
        }
    }
}