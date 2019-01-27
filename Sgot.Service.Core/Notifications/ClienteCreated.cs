using MediatR;
using Sgot.Service.Core.Commands;

namespace Sgot.Service.Core.Notifications
{
    public class ClienteCreated : Command, INotification
    {
        public bool IsSuccess { get; private set; }

        public ClienteCreated(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Name = GetType().Name;
        }

        public override string ToString()
        {
            var info = string.Format("ID: {0}\r\nCommandName: {1}\r\nCreated: {2}\r\nIsSuccess: {3}", MessageId, Name, DateCreated, IsSuccess);
            return info;
        }
    }
}
