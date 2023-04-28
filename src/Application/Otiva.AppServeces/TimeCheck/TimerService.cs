using Microsoft.Extensions.Logging;
using Otiva.AppServeces.Service.IdentityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Otiva.AppServeces.TimeCheck
{
    public class TimerService
    {
        private static Timer _timer;
        private IIdentityUserService _identityService;
        ILogger<TimerService> _logger;
        public TimerService(
            IIdentityUserService identityService,
            ILogger<TimerService> logger)
        { 
            _identityService = identityService;
            _logger = logger;
        }

        public void Start()
        {
            const int millisecond = 240000;//4 часа 
            _timer = new Timer(millisecond); 
            _timer.Elapsed += new ElapsedEventHandler(DeleteUnverifiedAccount);
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private async void DeleteUnverifiedAccount(object sender, ElapsedEventArgs e)
        {
            CancellationToken cancellation = new CancellationToken(); //заглушка, DeleteAsync ждет токен
            var delUsers = await _identityService.GetNotConfirmAccount();
            foreach (var userId in delUsers)
            {
                await _identityService.DeleteAsync(userId, cancellation);
            }

            _logger.LogInformation("Из базы данных были удалены пользователи, которые не подтвердили свою почту");
        }
    }
}
