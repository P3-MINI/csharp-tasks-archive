namespace Lab04.Services.Validators;

public sealed class EmailAddressValidator : Validator
{
    public override bool Validate(string? value)
    {
        return !string.IsNullOrWhiteSpace(value) &&
            value.Contains('@') &&
            value.EndsWith(".com");
    }
}