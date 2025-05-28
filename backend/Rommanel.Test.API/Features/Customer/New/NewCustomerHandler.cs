using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommanel.Test.API.Entities;
using Rommanel.Test.API.Infra.EntityFramework;
using Rommanel.Test.API.Infra.Exceptions;
using Rommanel.Test.API.Infra.Messages;

namespace Rommanel.Test.API.Features.Customer.New
{
    public class NewCustomerHandler
        (RommanelContext _context) : IRequestHandler<NewCustomerCommand>
    {

        public async Task Handle(NewCustomerCommand request, CancellationToken cancellationToken)
        {

            var customer = new Rommanel.Test.API.Entities.Customer
            {
                Name = request.Name,
                DocumentNumber = request.DocumentNumber,
                BirthDate = request.BirthDate,
                Cellphone = request.Cellphone,
                Email = request.Email,
                RegisterNumber = request.RegisterNumber!,
                CustomerType = request.CustomerType,
                CustomerStatus = Entities.Enums.CustomerStatus.Actived,
                CreatedAt = DateTime.Now
            };

            var customers = _context
                .Set<Rommanel.Test.API.Entities.Customer>()
                .AsNoTracking()
                .AsEnumerable();

            if(customer.CustomerAlreadyExists(customers))
                throw new AppException([Messages.CustomerAlreadyExists]);

            if (customer.CustomerType == Entities.Enums.CustomerType.Person && customer.IsAdult())
                throw new AppException([Messages.PersonIsNotAdult]);

            if (customer.CustomerType == Entities.Enums.CustomerType.Company && string.IsNullOrEmpty(customer.RegisterNumber))
                throw new AppException([Messages.InvalidRegisterNumber]);

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

            try
            {
                _context.Add(customer);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
