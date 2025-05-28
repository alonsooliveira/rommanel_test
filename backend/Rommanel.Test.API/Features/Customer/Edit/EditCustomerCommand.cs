using MediatR;
using Rommanel.Test.API.Entities.Enums;
using Rommanel.Test.API.Features.Customer.New;

namespace Rommanel.Test.API.Features.Customer.Edit
{
    public class EditCustomerCommand : NewCustomerCommand, IRequest
    {
        public required Guid Id { get; set; }
    }
}
