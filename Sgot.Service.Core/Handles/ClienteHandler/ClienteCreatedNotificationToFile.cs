using MediatR;
using Sgot.Service.Core.Notifications;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sgot.Service.Core.Handles.ClienteHandler
{
    public class ClienteCreatedNotificationToFile : INotificationHandler<ClienteCreated>
    {
        public Task Handle(ClienteCreated notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                string path = $"{Directory.GetCurrentDirectory()}/logNotifications.txt";
                using (StreamWriter sw = File.Exists(path) ? File.AppendText(path) : File.CreateText(path))
                    sw.WriteLineAsync($"{notification.ToString()}\r\n");
            });
        }
    }
}
