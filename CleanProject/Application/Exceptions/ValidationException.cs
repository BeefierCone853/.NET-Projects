using FluentValidation.Results;

namespace Application.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public List<string> Errors { get; set; } = [];

    public ValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Errors.Add(error.ErrorMessage);
        }
    }
}