using ErrorOr;
using FluentValidation.Results;

namespace MyRoad.Domain.Common;

public static class ErrorExtensions
{
    public static List<Error> ExtractErrors(this ValidationResult result)
    {
        if (result.Errors.Count == 0)
        {
            return [];
        }

        return result.Errors.Select(e => Error.Validation(
                code: string.IsNullOrWhiteSpace(e.ErrorCode) ? "Validation.General" : e.ErrorCode,
                description: $"{e.PropertyName}: {e.ErrorMessage}"
            ))
            .ToList();
    }
}
