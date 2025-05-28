using MediatR;

namespace Rommanel.Test.API.Features.Customer.Remove
{
    public class RemoveCustomerCommand : IRequest
    {
        public required Guid Id { get; set; }
    }
}
