using FluentValidation;
using Rommanel.Test.API.Entities.Enums;



using Rommanel.Test.API.Infra.Messages;


namespace Rommanel.Test.API.Features.Customer.New
{
    public class NewCustomerValidation : AbstractValidator<NewCustomerCommand>
    {
        public NewCustomerValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(400)
                .WithMessage(Messages.InvalidName);

            RuleFor(x => x.Email)
               .EmailAddress()
               .WithMessage(Messages.InvalidEmail);

            RuleFor(x => x.DocumentNumber).Must(BeAValidPersonDocumentNumber)
               .When(p => p.CustomerType == Entities.Enums.CustomerType.Person)
               .WithMessage(Messages.InvalidPersonDocumentNumber);

            RuleFor(x => x.DocumentNumber).Must(BeAValidCompanyDocumentNumber)
               .When(p => p.CustomerType == Entities.Enums.CustomerType.Company)
               .WithMessage(Messages.InvalidPersonDocumentNumber);

            RuleFor(x => x.BirthDate)
               .NotNull()
               .Must(BeAValidDate)
               .WithMessage(Messages.InvalidName);

            RuleFor(x => x.CustomerType)
              .NotNull()
              .Must(BeAValidCustomerType)
              .WithMessage(Messages.InvalidCustomerType);
        }

        private bool BeAValidPersonDocumentNumber(string cpf)
        {
            int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        private bool BeAValidCompanyDocumentNumber(string cnpj)
        {
            int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        private bool BeAValidDate(DateTime date)
        {
            return DateTime.TryParse(date.ToShortDateString(), out _);
        }

        private bool BeAValidCustomerType(CustomerType customerType)
        {
            var types = new List<CustomerType> { CustomerType.Person, CustomerType.Company };
            return types.Contains(customerType);
        }

    }
}
