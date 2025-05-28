using MediatR;
using Rommanel.Test.API.Infra.EntityFramework;

namespace Rommanel.Test.API.Features.Customer.Remove
{
    public class RemoveCustomerHandler
        (RommanelContext _context) : IRequestHandler<RemoveCustomerCommand>
    {
        public async Task Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _context.Set<Rommanel.Test.API.Entities.Customer>()
                .SingleOrDefault(x => x.Id == request.Id);

            if (customer is null)
                throw new Exception("Customer not found");

            customer.CustomerStatus = Entities.Enums.CustomerStatus.Removed;

            _context.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
