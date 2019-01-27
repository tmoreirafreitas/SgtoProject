using Sgot.Domain.Entities;
using Sgot.Service.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service.Core.Responses
{
    public class EntityResponse
    {
        public bool IsCreated { get; private set; }
        public bool IsUpdated { get; private set; }
        public bool IsDeleted { get; private set; }        
        public string Message { get; private set; }
        public Command Command { get; private set; }
        public Entity Item { get; private set; }

        public EntityResponse(bool isCreated, bool isUpdated, bool isDeleted, Entity item, string message = "", Command command = null)
        {
            IsCreated = isCreated;
            IsUpdated = isUpdated;
            IsDeleted = isDeleted;
            Item = item;
            Message = message;
            Command = command;
        }
    }
}
