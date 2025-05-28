using FluentValidation;

namespace Rommanel.Test.API.Features.Customer.Remove
{
    public class RemoveCustomerValidation : AbstractValidator<RemoveCustomerCommand>
    {
        public RemoveCustomerValidation()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Invalid Customer");
        }
    }
}
