using Microsoft.EntityFrameworkCore;
using Rommanel.Test.API.Infra.EntityFramework;
using Rommanel.Test.API.Queries.Interfaces;

namespace Rommanel.Test.API.Queries.Customer
{
    public class CustomerQuery
        (RommanelContext _context) : ICustomerQuery
    {
        public async Task<CustomerResult> GetCustomerById(Guid id)
        {
            return await _context
                .Set<Rommanel.Test.API.Entities.Customer>()
                .Where(p => p.Id == id)
                .Select(p => new CustomerResult
                {
                    Id = p.Id,
                    Name = p.Name,
                    DocumentNumber = p.DocumentNumber,
                    Email = p.Email,
                    Cellphone = p.Cellphone,
                    CustomerStatus = p.CustomerStatus,
                    CustomerType = p.CustomerType,
                    RegisterNumber = p.RegisterNumber,
                    BirthDate = p.BirthDate,
                    Addresses = p.Addresses.Select(a => new AddressResult
                    {
                        Id = a.Id,
                        Name = a.Name,
                        PostalCode = a.PostalCode,
                        Street = a.Street,
                        Number = a.Number,
                        Complement = a.Complement,
                        Neighborhood = a.Neighborhood,
                        City = a.City,
                        State = a.State,
                    })
                })
                .FirstOrDefaultAsync() ?? new CustomerResult();
        }
        public async Task<IEnumerable<CustomerResult>> GetAllCustomer()
        {
            return await _context
                .Set<Rommanel.Test.API.Entities.Customer>()
                .Where(p => p.CustomerStatus == Entities.Enums.CustomerStatus.Actived)
                .Select(p => new CustomerResult
                {
                    Id = p.Id,
                    Name = p.Name,
                    DocumentNumber = p.DocumentNumber,
                    Email = p.Email,
                    Cellphone = p.Cellphone,
                    CustomerStatus = p.CustomerStatus,
                    RegisterNumber = p.RegisterNumber,
                    CustomerType = p.CustomerType,
                    BirthDate = p.BirthDate,
                    Addresses = p.Addresses.Select(a => new AddressResult
                    {
                        Id = a.Id,
                        Name = a.Name,
                        PostalCode = a.PostalCode,
                        Street = a.Street,
                        Number = a.Number,
                        Complement = a.Complement,
                        Neighborhood = a.Neighborhood,
                        City = a.City,
                        State = a.State,
                    })
                }).ToListAsync() ?? [];
        }
    }
}
