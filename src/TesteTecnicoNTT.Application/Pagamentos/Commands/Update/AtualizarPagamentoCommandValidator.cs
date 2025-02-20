using FluentValidation;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Update
{
    public class AtualizarPagamentoCommandValidator : AbstractValidator<AtualizarPagamentoCommand>
    {
        public AtualizarPagamentoCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("O ID do pagamento é obrigatório.");

            RuleFor(p => p.NovoValor)
                .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");

            RuleFor(p => p.NovaData)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("A data do pagamento não pode ser no passado.");

            RuleFor(p => p.NovoStatus)
                .IsInEnum().WithMessage("Status inválido. Os valores permitidos são: Pago, Atrasado e A Vencer.");
        }
    }
}
