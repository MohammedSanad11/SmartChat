using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Commands.DeleteUsers
{
    public class DeleteCommandHandleUser : IRequestHandler<DeleteCommandUser, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public DeleteCommandHandleUser(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(DeleteCommandUser request)
        {
            var user = await _uintOfWork._UsersRepository.GetByConditionAsync(u=>u.Id == request.Id);

            if (user == null) 
                return false;

             _uintOfWork._UsersRepository.Delete(user);
            
            _uintOfWork.SaveChange();

            return true;
        }
    }
}
