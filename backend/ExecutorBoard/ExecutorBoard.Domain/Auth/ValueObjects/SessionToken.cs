namespace ExecutorBoard.Domain.Auth.ValueObjects;

public sealed class SessionToken(string value)
{
    private readonly string _value = value;

    public string Value() => _value;

    public static SessionToken From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Session token is required");
        }

        return new SessionToken(value);
    }
}
