using Alexdric.Application.Common;

namespace Alexdric.Application.Exceptions;

public class CustomValidationException : Exception
{
    public IEnumerable<BaseError> Errors { get; }

    public CustomValidationException()
        : base("One or more validation failures have occured.")
    {
        Errors = new List<BaseError>();
    }

    public CustomValidationException(IEnumerable<BaseError> errors)
        : this()
    {
        Errors = errors;
    }
}