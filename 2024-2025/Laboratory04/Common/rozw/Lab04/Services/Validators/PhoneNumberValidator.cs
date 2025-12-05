using System.Text.RegularExpressions;

namespace Lab04.Services.Validators;

public sealed class PhoneNumberValidator : Validator
{
    private static readonly int PhoneNumberLength = 9;

    public override bool Validate(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        if (value.Length != PhoneNumberLength)
            return false;

        foreach (var d in value)
        {
            if (!char.IsDigit(d))
                return false;
        }

        return true;
    }
}