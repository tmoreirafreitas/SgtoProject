using MediatR;
using Microsoft.Extensions.Logging;
using Sgot.Service.Core.Commands.AccountRequest;
using Sgot.Service.Core.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles
{
    public class AccountLoggedNotificationHandle : INotificationHandler<AccountLogged>
    {
        private readonly ILogger<LoginAccount> _logger;

        public AccountLoggedNotificationHandle(ILogger<LoginAccount> logger)
        {
            _logger = logger;
        }

        public Task Handle(AccountLogged notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogInformation(notification.Message);
            });
        }
    }
}
