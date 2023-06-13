using Microsoft.Extensions.Logging;
using Otiva.AppServeces.Service.IdentityService;
using Otiva.AppServeces.Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Otiva.AppServeces.TimeCheck
{
    /// <summary>
    /// Таймер приложения
    /// </summary>
    public class TimerService
    {
        private static Timer _timer;
        private IIdentityUserService _identityService;
        private ILogger<TimerService> _logger;
        private IUserService _userService;
        public TimerService(
            IIdentityUserService identityService,
            IUserService userService,
            ILogger<TimerService> logger)
        { 
            _identityService = identityService;
            _logger = logger;
            _userService = userService;
        }

        public void Start()
        {
            const int millisecond = 18000000;//5 часов 
            _timer = new Timer(millisecond); 
            _timer.Elapsed += new ElapsedEventHandler(DeleteUnverifiedAccount);
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        /// <summary>
        /// Удалит аккаунты пользователей, которые не подтвердили свою почту в течении 24 часов 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteUnverifiedAccount(object sender, ElapsedEventArgs e)
        {
            int k = 0;
            CancellationToken cancellation = new CancellationToken(); //заглушка, DeleteAsync ждет токен
            var delUsers = await _identityService.GetNotConfirmAccount();
            foreach (var userId in delUsers)
            {
                await _userService.DeleteAsync(userId, cancellation);
                k++;
            }
            _logger.LogInformation($"Из базы данных были удалены {k} пользователей, которые не подтвердили свою почту");
        }
    }
}
