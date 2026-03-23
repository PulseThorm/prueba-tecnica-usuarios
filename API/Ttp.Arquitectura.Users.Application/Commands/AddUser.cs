using Mapster;
using System.Net.Http.Json;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class AddUserCommand
    {
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }

        public List<Address> Addresses { get; set; }
    }
    //Reglas:   Parámetros obligatorios
    //          Enviar un Mail al final del ingreso
    public class AddUserHandler(IGenericRepository<User> user, HttpClient httpClient)
    {
        private IGenericRepository<User> _user { get; } = user;
        private HttpClient _httpClient { get; } = httpClient;

        public void Handle(AddUserCommand command)
        {
             if (string.IsNullOrEmpty(command.FullName))
                throw new Exception("El nombre es requerido");

            if (string.IsNullOrEmpty(command.Email))
                throw new Exception("El email es requerido");
                
            var userEntity = command.Adapt<User>();
            userEntity.IsActive = true;

            if (userEntity.Addresses != null && userEntity.Addresses.Any())
            {
                var primaryCount = userEntity.Addresses.Count(x => x.IsPrimary);

                if (primaryCount > 1)
                    throw new Exception("Solo puede existir una dirección principal");

                if (primaryCount == 0)
                    userEntity.Addresses.First().IsPrimary = true;
            }
           

            _user.Insert(userEntity);
            _user.Save();
            var emailRequest = new SendEmailRequest
            {
                To = userEntity.Email,
                Body = "Bienvenido nuevo usuario al sistema"
            };

            try
            {
                _httpClient.PostAsJsonAsync("http://emailsender/", emailRequest).Wait();
            }
            catch(Exception ex)
            {
                // No dispongo de un servicio de mensajería pero no se interrumpe la creación del usuario
                
                Console.WriteLine($"Error enviando email: {ex.Message}");
            }
        }
    }
}