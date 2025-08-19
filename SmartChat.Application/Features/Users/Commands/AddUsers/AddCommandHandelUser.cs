using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Commands.AddUsers
{
    public class AddCommandHandelUser : IRequestHandler<AddCommandUser, Guid>
    {
        private readonly IUintOfWork _uintOfWork;

        public AddCommandHandelUser(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<Guid> Handle(AddCommandUser request)
        {
            var role = await _uintOfWork._RolesRepository.GetByConditionAsync(r => r.Id == request.RoleId);

            //if (role == null)
            //    throw new Exception("Invalid Role Id");


            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UesrName = request.UserName,
                Email = request.Email,
                PassWord = request.Password,
                RoleId = request.RoleId,
                CreatedAt = DateTime.UtcNow,
            };

            await _uintOfWork._UsersRepository.AddAsync(user);
            await _uintOfWork.SaveChangeAsync();

            return user.Id;

        }
    }
}
