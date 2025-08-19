using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Messeges.Qyerys.GetMessageById
{
    public class GetMessageByIdQuery:IRequest<MessageDto>
    {
        public Guid Id { get; set; }
        public GetMessageByIdQuery(Guid id)
        {
            Id = id;
        }

  
     
    }
}
