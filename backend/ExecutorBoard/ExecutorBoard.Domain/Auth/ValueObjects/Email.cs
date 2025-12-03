namespace ExecutorBoard.Domain.Auth.ValueObjects;

public sealed class Email(string value)
{
    private readonly string _value = Normalize(value);

    public string Value() => _value;

    public override bool Equals(object? obj)
    {
        if (obj is not Email other)
        {
            return false;
        }

        return _value == other._value;
    }

    public override int GetHashCode() => _value.GetHashCode();

    public static bool operator ==(Email? left, Email? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Email? left, Email? right) => !(left == right);

    public static Email From(string value) => new(value);

    private static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Email is required");
        }

        var trimmed = value.Trim();

        if (!trimmed.Contains('@'))
        {
            throw new DomainException("Email is invalid");
        }

        return trimmed.ToLowerInvariant();
    }
}
