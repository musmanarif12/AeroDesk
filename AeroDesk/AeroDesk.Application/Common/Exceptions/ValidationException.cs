using FluentValidation.Results;

namespace AeroDesk.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(
            IEnumerable<ValidationFailure> failures)
        {
            Errors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(x => x.ErrorMessage).ToArray()
                );
        }
    }
}