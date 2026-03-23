namespace Ttp.Arquitectura.Users.WebApi.Models.Request
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }

        public List<AddressRequest> Addresses { get; set; }
    }
}
