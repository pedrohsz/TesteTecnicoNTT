using FluentValidation;

namespace TesteTecnicoNTT.Application.Pagamentos.Commands.Create
{
    public class AdicionarPagamentoCommandValidator : AbstractValidator<AdicionarPagamentoCommand>
    {
        public AdicionarPagamentoCommandValidator()
        {
            RuleFor(p => p.ClienteId)
                .NotEmpty().WithMessage("O ClienteId é obrigatório.");

            RuleFor(p => p.NumeroContrato)
                .NotEmpty().WithMessage("O número do contrato não pode ser vazio.")
                .MaximumLength(50).WithMessage("O número do contrato deve ter no máximo 50 caracteres.");

            RuleFor(p => p.Parcela)
                .GreaterThan(0).WithMessage("A parcela deve ser maior que zero.");

            RuleFor(p => p.Valor)
                .GreaterThan(0).WithMessage("O valor do pagamento deve ser maior que zero.");

            //RuleFor(p => p.DataPagamento)
            //    .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("A data do pagamento não pode ser no passado.");

            RuleFor(p => p.Status)
                .IsInEnum().WithMessage("Status inválido. Os valores permitidos são: Pago, Atrasado e A Vencer.");
        }
    }
}
