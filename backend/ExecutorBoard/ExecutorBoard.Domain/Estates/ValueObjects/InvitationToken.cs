namespace ExecutorBoard.Domain.Estates.ValueObjects;

public sealed class InvitationToken : IEquatable<InvitationToken>
{
    private readonly string _value;

    private InvitationToken(string value)
    {
        _value = value;
    }

    public string Value()
    {
        return _value;
    }

    public static InvitationToken From(string value)
    {
        return new InvitationToken(value);
    }

    public bool Equals(InvitationToken? other)
    {
        if (other is null)
        {
            return false;
        }

        return string.Equals(_value, other._value, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as InvitationToken);
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(_value);
    }
}
