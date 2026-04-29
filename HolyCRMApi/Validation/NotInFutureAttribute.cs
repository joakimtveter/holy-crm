using System.ComponentModel.DataAnnotations;

namespace HolyCRMApi.Validation;

public class NotInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateOnly date && date > DateOnly.FromDateTime(DateTime.UtcNow))
            return new ValidationResult(ErrorMessage ?? "Date of birth cannot be in the future.");

        return ValidationResult.Success;
    }
}
