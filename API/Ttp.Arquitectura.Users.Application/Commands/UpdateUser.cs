using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class UpdateUserCommand
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }

        public List<Address> Addresses { get; set; }
    }

    public class UpdateUserHandler(
        IGenericRepository<User> user,
        IGenericRepository<Address> address)
    {
        private IGenericRepository<User> _user { get; } = user;
        private IGenericRepository<Address> _address { get; } = address;

        public void Handle(UpdateUserCommand command)
        {
            var userToUpdate = _user.Get(filter: x => x.Id == command.Id).FirstOrDefault();

            if (userToUpdate == null)
                throw new Exception("Usuario no encontrado");

            if (string.IsNullOrEmpty(command.FullName))
                throw new Exception("El nombre es requerido");

            if (string.IsNullOrEmpty(command.Email))
                throw new Exception("El email es requerido");

            userToUpdate.FullName = command.FullName;
            userToUpdate.Birth = command.Birth;
            userToUpdate.Email = command.Email;

            _user.Update(userToUpdate);
            _user.Save();

            if (command.Addresses != null && command.Addresses.Any())
            {
                var newPrimaryCount = command.Addresses.Count(x => x.IsPrimary);

                if (newPrimaryCount > 1)
                    throw new Exception("Solo puede existir una dirección principal");

                if (newPrimaryCount == 1)
                {
                    var existingAddresses = _address.Get(filter: x => x.UserId == userToUpdate.Id).ToList();

                    foreach (var existingAddress in existingAddresses)
                    {
                        existingAddress.IsPrimary = false;
                        _address.Update(existingAddress);
                    }

                    _address.Save();
                }

                foreach (var newAddress in command.Addresses)
                {
                    _address.Insert(new Address
                    {
                        Id = Guid.NewGuid(),
                        Street = newAddress.Street,
                        IsPrimary = newAddress.IsPrimary,
                        UserId = userToUpdate.Id
                    });
                }

                _address.Save();
            }
        }
    }
}