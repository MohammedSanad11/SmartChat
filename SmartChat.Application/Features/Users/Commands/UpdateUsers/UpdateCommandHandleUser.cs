using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Commands.UpdateUsers
{
    public class UpdateCommandHandleUser : IRequestHandler<UpdateCommandUser, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public UpdateCommandHandleUser(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(UpdateCommandUser request)
        {
            var user = await _uintOfWork._UsersRepository.GetByConditionAsync
                (u => u.Id == request.Id);

            if (user == null)
                return false;

            user.PassWord = request.Password;
            user.Name = request.Name;
            user.Email = request.Email;
            user.RoleId = request.RoleId;

            _uintOfWork._UsersRepository.Update(user);
            _uintOfWork.SaveChange();

            return true;
        }
    }
}
