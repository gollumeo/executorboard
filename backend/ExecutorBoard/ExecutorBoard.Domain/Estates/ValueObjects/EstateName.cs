namespace ExecutorBoard.Domain.Estates.ValueObjects;

public sealed class EstateName(string value)
{
    private string _value { get; } = value;

    public string Value() => _value;

    public override bool Equals(object? obj)
    {
        if (obj is not EstateName other)
        {
            return false;
        }

        return Value() == other.Value();
    }

    public override int GetHashCode() => Value().GetHashCode();

    public static bool operator ==(EstateName? left, EstateName? right)
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

    public static bool operator !=(EstateName? left, EstateName? right) => !(left == right);

    public static EstateName From(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new DomainException("Display name is required");
        }

        var trimmedDisplayName = displayName.Trim();

        if (trimmedDisplayName.Length < 2)
        {
            throw new DomainException("Display name is too short");
        }

        var normalized = string.Join(
            " ",
            trimmedDisplayName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(word =>
                {
                    var lower = word.ToLowerInvariant();
                    return char.ToUpperInvariant(lower[0]) + lower[1..];
                }));

        return new EstateName(normalized);
    }
}
