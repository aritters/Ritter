using Ritter.Infra.Crosscutting;
using System.Collections.Generic;
using System.Linq;

namespace Ritter.Domain.Validations
{
    public sealed class ValidationResult
    {
        public ValidationResult(params ValidationError[] errors)
            : this(errors as IEnumerable<ValidationError>)
        {
        }

        public ValidationResult(IEnumerable<ValidationError> errors)
        {
            Errors = errors?
                .GroupBy(p => new { p.Property, p.Message })
                .Select(p => p.First())
                .ToList()
                ?? new List<ValidationError>();
        }

        public bool IsValid
            => Errors.Count == 0;

        public ICollection<ValidationError> Errors { get; } = new List<ValidationError>();

        public void AddError(string message)
            => Errors.Add(new ValidationError(message));

        public void AddError(string property, string message)
            => Errors.Add(new ValidationError(property, message));

        internal ValidationResult Append(ValidationResult appendResult)
            => new ValidationResult(appendResult.Errors.Union(Errors));

        private void AddError(ValidationError error)
            => Errors.Add(error);
    }
}
