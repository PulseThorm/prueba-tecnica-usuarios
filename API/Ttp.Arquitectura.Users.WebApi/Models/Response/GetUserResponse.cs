namespace Ttp.Arquitectura.Users.WebApi.Models.Response
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public List<AddressResponse> Addresses { get; set; }
    }
    public class AddressResponse
    {
        public string Street { get; set; }
        public bool IsPrimary { get; set; }
    }
}
