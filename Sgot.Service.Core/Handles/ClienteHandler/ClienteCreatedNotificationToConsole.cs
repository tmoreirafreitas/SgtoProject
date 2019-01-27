using MediatR;
using Microsoft.Extensions.Logging;
using Sgot.Service.Core.Commands.ClienteRequest;
using Sgot.Service.Core.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles.ClienteHandler
{
    public class ClienteCreatedNotificationToConsole : INotificationHandler<ClienteCreated>
    {
        private readonly ILogger<CreateCliente> _logger;

        public ClienteCreatedNotificationToConsole(ILogger<CreateCliente> logger)
        {
            _logger = logger;
        }

        public Task Handle(ClienteCreated notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                _logger.LogDebug(notification.ToString());
            });
        }
    }
}
