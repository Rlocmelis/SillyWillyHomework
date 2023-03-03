using FluentValidation.Results;

namespace SillyWillyHomework.Validation
{
    public class ValidationErrorResponse
    {
        public IEnumerable<ValidationError> Errors { get; set; }

        public ValidationErrorResponse(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }

        public ValidationErrorResponse(ValidationError error)
        {
            Errors = new List<ValidationError> { error };
        }
    }
}
