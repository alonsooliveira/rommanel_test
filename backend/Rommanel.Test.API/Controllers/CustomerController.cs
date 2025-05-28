using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rommanel.Test.API.Features.Customer.Edit;
using Rommanel.Test.API.Features.Customer.New;
using Rommanel.Test.API.Features.Customer.Remove;
using Rommanel.Test.API.Queries.Customer;
using Rommanel.Test.API.Queries.Interfaces;

namespace Rommanel.Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(
        IMediator _mediator,
        ICustomerQuery _query) : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<CustomerResult>> GetAsync()
        {
            return await _query.GetAllCustomer();
        }

        [HttpGet("{id}")]
        public async Task<CustomerResult> GetByIdAsync(Guid id)
        {
            return await _query.GetCustomerById(id);
        }

        [HttpPost]
        public async Task PostAsync([FromBody] NewCustomerCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpPut]
        public async Task PutAsync([FromBody] EditCustomerCommand command)
        {
            await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _mediator.Send(new RemoveCustomerCommand { Id = id });
        }
    }
}
