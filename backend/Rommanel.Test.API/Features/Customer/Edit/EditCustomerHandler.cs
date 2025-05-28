using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommanel.Test.API.Entities;
using Rommanel.Test.API.Infra.EntityFramework;
using Rommanel.Test.API.Infra.Exceptions;
using Rommanel.Test.API.Infra.Messages;

namespace Rommanel.Test.API.Features.Customer.Edit
{
    public class EditCustomerHandler
        (RommanelContext _context) : IRequestHandler<EditCustomerCommand>
    {
        public async Task Handle(EditCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Set<Rommanel.Test.API.Entities.Customer>()
                .Include(p => p.Addresses)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (customer is null)
                throw new AppException([Messages.CustomerNotFound]);

            customer.Name = request.Name;
            customer.DocumentNumber = request.DocumentNumber;
            customer.BirthDate = request.BirthDate;
            customer.Cellphone = request.Cellphone;
            customer.Email = request.Email;

            var customers = _context
                .Set<Rommanel.Test.API.Entities.Customer>()
                .AsNoTracking()
                .AsEnumerable();

            if (customer.CustomerAlreadyExists(customers))
                throw new AppException([Messages.CustomerAlreadyExists]);

            if (customer.CustomerType == Entities.Enums.CustomerType.Person && customer.IsAdult())
                throw new AppException([Messages.PersonIsNotAdult]);

            if (customer.CustomerType == Entities.Enums.CustomerType.Company && string.IsNullOrEmpty(customer.RegisterNumber))
                throw new AppException([Messages.InvalidRegisterNumber]);

            _context.RemoveRange(customer.Addresses);

            foreach (var address in request.Addresses)
            {
                var newAddress = new Address
                {
                    Name = address.Name,
                    PostalCode = address.PostalCode,
                    Street = address.PostalCode,
                    Number = address.Number,
                    Complement = address.Complement,
                    Neighborhood = address.Neighborhood,
                    City = address.City,
                    State = address.State,
                    CreatedAt = DateTime.Now
                };

                customer.AddAddress(newAddress);
            }


            _context.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
