using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class DeleteUserCommand
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserHandler(IGenericRepository<User> user)
    {
        private IGenericRepository<User> _user { get; } = user;

        public void Handle(DeleteUserCommand command)
        {
            var userToDelete = _user.Get().FirstOrDefault(x => x.Id == command.Id);

            if (userToDelete == null)
                throw new Exception("Usuario no encontrado");

            userToDelete.IsActive = false;
            _user.Update(userToDelete);
            _user.Save();
        }
    }
}