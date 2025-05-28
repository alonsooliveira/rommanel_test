using System.Reflection.Metadata;

namespace Rommanel.Test.API.Entities
{
    public class Address : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public required string PostalCode { get; set; }
        public required string Name { get; set; }
        public required string Street { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public required string Neighborhood { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
