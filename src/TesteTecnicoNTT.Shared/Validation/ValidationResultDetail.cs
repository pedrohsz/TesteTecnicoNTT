using FluentValidation.Results;

namespace TesteTecnicoNTT.Common.Validation
{
    public class ValidationResultDetail
    {
        public bool IsValid { get; set; } // Indica se a validação foi bem-sucedida
        public IEnumerable<ValidationErrorDetail> Errors { get; set; } = []; // Lista de erros

        public ValidationResultDetail() { }

        // Converte um ValidationResult do FluentValidation para este formato
        public ValidationResultDetail(ValidationResult validationResult)
        {
            IsValid = validationResult.IsValid;
            Errors = validationResult.Errors.Select(o => (ValidationErrorDetail)o);
        }
    }
}
