using System.Text.RegularExpressions;

namespace Lab04.Services.Validators;

public sealed class NameValidator : Validator
{
    public override bool Validate(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        foreach (var c in value)
        {
            if (!char.IsLetter(c))
                return false;
        }

        return true;
    }
}