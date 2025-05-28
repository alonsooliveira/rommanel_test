using MediatR;
using Rommanel.Test.API.Entities.Enums;

namespace Rommanel.Test.API.Features.Customer.New
{
    public class NewCustomerCommand : IRequest
    {
        public required string Name { get; set; }
        public required string DocumentNumber { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Cellphone { get; set; }
        public required string Email { get; set; }
        public string? RegisterNumber { get; set; }
        public CustomerType CustomerType { get; set; }
        public CustomerStatus CustomerStatus { get; set; }
        public IEnumerable<NewAddressCommand> Addresses { get; set; } = null!;
    }

    public class NewAddressCommand
    {
        public required string PostalCode { get; set; }
        public required string Name { get; set; }
        public required string Street { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public required string Neighborhood { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
    }
}
