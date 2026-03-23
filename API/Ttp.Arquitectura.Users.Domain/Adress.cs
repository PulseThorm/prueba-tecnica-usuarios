namespace Ttp.Arquitectura.Users.Domain
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public bool IsPrimary { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}