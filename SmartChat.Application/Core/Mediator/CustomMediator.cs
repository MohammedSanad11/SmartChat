using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Core.Mediator
{
    public class CustomMediator : ICustomMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = (typeof(IRequestHandler<,>))
                .MakeGenericType(request.GetType(),typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);

            if (handler == null)
                throw new Exception($"Handler for {request.GetType().Name} not found.");

            var method = handlerType.GetMethod("Handle");

            var result = method.Invoke(handler,new object[] { request });

            return await (Task<TResponse>)result;
        }
    }
}
