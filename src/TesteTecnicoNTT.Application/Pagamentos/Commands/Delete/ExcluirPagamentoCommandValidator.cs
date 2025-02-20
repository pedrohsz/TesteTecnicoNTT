using FluentValidation;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Delete
{
    public class ExcluirPagamentoCommandValidator : AbstractValidator<ExcluirPagamentoCommand>
    {
        public ExcluirPagamentoCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("O ID do pagamento é obrigatório.");
        }
    }
}
