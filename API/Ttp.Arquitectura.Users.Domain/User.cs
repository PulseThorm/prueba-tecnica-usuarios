namespace Ttp.Arquitectura.Users.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;

        public List<Address> Addresses { get; set; } = new();
    }
}