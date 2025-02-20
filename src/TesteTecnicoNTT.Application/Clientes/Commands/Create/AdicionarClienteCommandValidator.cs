using FluentValidation;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Create
{
    public class AdicionarClienteCommandValidator : AbstractValidator<AdicionarClienteCommand>
    {
        public AdicionarClienteCommandValidator()
        {
            RuleFor(c => c.CpfCnpj)
                .NotEmpty().WithMessage("O CPF ou CNPJ é obrigatório.")
                .Must(ValidarCpfCnpj).WithMessage("CPF ou CNPJ inválido.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");

            RuleFor(c => c.NumeroContrato)
                .NotEmpty().WithMessage("O número do contrato é obrigatório.")
                .MaximumLength(50).WithMessage("O número do contrato não pode exceder 50 caracteres.");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .MaximumLength(100).WithMessage("A cidade não pode exceder 100 caracteres.");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .MaximumLength(50).WithMessage("O estado não pode exceder 50 caracteres.");

            RuleFor(c => c.RendaBruta)
                .GreaterThan(0).WithMessage("A renda bruta deve ser maior que zero.");
        }

        private bool ValidarCpfCnpj(string cpfCnpj)
        {
            // Aqui você pode adicionar um algoritmo de validação real de CPF/CNPJ
            return !string.IsNullOrEmpty(cpfCnpj) && (cpfCnpj.Length == 11 || cpfCnpj.Length == 14);
        }
    }
}
