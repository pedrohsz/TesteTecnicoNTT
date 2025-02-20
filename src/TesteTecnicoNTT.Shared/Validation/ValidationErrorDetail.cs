using FluentValidation.Results;

namespace TesteTecnicoNTT.Common.Validation
{
    public class ValidationErrorDetail
    {
        public string Error { get; init; } = string.Empty; // Código do erro
        public string Detail { get; init; } = string.Empty; // Mensagem detalhada

        // Converte um ValidationFailure do FluentValidation para este formato
        public static explicit operator ValidationErrorDetail(ValidationFailure validationFailure)
        {
            return new ValidationErrorDetail
            {
                Detail = validationFailure.ErrorMessage,
                Error = validationFailure.ErrorCode
            };
        }
    }
}