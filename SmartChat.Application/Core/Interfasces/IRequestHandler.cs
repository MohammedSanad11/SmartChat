using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Core.Interfasces;

public interface IRequestHandler<TRequest, TResponse>
    where TRequest: IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request);
}
