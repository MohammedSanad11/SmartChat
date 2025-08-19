using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Messeges.Commands.UpdateMessage
{
    public class UpdateMessageCommand:IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
    }
}
