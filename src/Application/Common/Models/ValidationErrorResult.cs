namespace Application.Common.Models;

public sealed class ValidationErrorResult
{
    public ValidationErrorResult(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }
}