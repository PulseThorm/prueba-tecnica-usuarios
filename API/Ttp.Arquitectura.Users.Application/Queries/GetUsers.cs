using Mapster;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Domain;

namespace Ttp.Arquitectura.Users.Application.Queries
{
    public class GetUsersQuery
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
    public class AddressDto
    {
        public string Street { get; set; }
        public bool IsPrimary { get; set; }
    }
    
    public class GetUsersHandler(IGenericRepository<User> user)
    {
        private IGenericRepository<User> _user { get; } = user;

        public List<GetUsersQuery> Handle(int page, int pageSize)
        {
            return _user
                .Get(includeProperties: "Addresses")
                .Where(x => x.IsActive)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Adapt<List<GetUsersQuery>>();
        }
    }
}