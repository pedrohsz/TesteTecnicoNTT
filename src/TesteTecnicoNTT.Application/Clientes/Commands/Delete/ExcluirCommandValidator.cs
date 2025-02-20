using FluentValidation;

namespace TesteTecnicoNTT.Application.Clientes.Commands.Delete
{
    public class ExcluirClienteCommandValidator : AbstractValidator<ExcluirClienteCommand>
    {
        public ExcluirClienteCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O ID do cliente é obrigatório.");
        }
    }
}
