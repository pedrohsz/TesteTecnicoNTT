using FluentValidation;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Update
{
    public class AtualizarClienteCommandValidator : AbstractValidator<AtualizarClienteCommand>
    {
        public AtualizarClienteCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .MaximumLength(100).WithMessage("O nome não pode exceder 100 caracteres.");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .MaximumLength(100).WithMessage("A cidade não pode exceder 100 caracteres.");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .MaximumLength(50).WithMessage("O estado não pode exceder 50 caracteres.");

            RuleFor(c => c.RendaBruta)
                .GreaterThan(0).WithMessage("A renda bruta deve ser maior que zero.");
        }
    }
}
