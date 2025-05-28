using Rommanel.Test.API.Entities.Enums;
using Rommanel.Test.API.Features.Customer.New;

namespace Rommanel.Test.API.Queries.Customer
{
    public class CustomerResult
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? DocumentNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Cellphone { get; set; }
        public string? Email { get; set; }
        public string? RegisterNumber { get; set; }
        public CustomerType CustomerType { get; set; }
        public CustomerStatus CustomerStatus { get; set; }
        public IEnumerable<AddressResult> Addresses { get; set; } = null!;
    }

    public class AddressResult
    {
        public Guid Id { get; set; }
        public string? PostalCode { get; set; }
        public string? Name { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
