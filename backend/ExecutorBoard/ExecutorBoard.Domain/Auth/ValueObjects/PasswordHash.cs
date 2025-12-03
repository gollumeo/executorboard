namespace ExecutorBoard.Domain.Auth.ValueObjects;

public sealed class PasswordHash(string value)
{
    public string Value() => value;

    public static PasswordHash From(string value) => new(value);
}
