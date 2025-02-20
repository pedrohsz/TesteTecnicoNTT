using TesteTecnicoNTT.Common.Validation;

namespace TesteTecnicoNTT.Domain.Common
{
    public abstract class BaseEntity : IComparable<BaseEntity>
    {
        public Guid Id { get; private init; }


        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        protected BaseEntity() { }

        public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
        {
            return Validator.ValidateAsync(this);
        }

        public int CompareTo(BaseEntity? other)
        {
            if (other == null)
            {
                return 1;
            }

            return other!.Id.CompareTo(Id);
        }
    }
}
