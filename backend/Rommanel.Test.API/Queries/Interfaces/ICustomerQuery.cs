using Rommanel.Test.API.Queries.Customer;

namespace Rommanel.Test.API.Queries.Interfaces
{
    public interface ICustomerQuery
    {
        Task<CustomerResult> GetCustomerById(Guid Id);

        Task<IEnumerable<CustomerResult>> GetAllCustomer();
    }
}
