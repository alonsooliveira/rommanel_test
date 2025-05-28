using Rommanel.Test.API.Entities.Enums;

namespace Rommanel.Test.API.Entities
{
    public class Customer : BaseEntity
    {
        public required string Name { get; set; }
        public required string DocumentNumber { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string Cellphone { get; set; }
        public required string Email { get; set; }
        public string? RegisterNumber { get; set; }
        public CustomerType CustomerType { get; set; }
        public CustomerStatus CustomerStatus { get; set; }
        public ICollection<Address> Addresses { get; set; }

        public Customer()
        {
            Addresses = [];
        }
        public void AddAddress(Address address)
        {
            Addresses.Add(address);
        }

        public bool CustomerAlreadyExists(IEnumerable<Customer> customers)
        {
            return customers
                .Where(p => (p.DocumentNumber == DocumentNumber || p.Email == Email) && p.Id != Id)
                .Any();
        }

        public bool IsAdult()
        {
            var age = new DateTime(DateTime.Now.Subtract(BirthDate).Ticks).Year - 1;
            return age < 18;
        }
    }
}
