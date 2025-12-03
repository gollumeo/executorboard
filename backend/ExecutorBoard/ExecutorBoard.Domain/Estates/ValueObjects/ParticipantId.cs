namespace ExecutorBoard.Domain.Estates.ValueObjects;

public sealed class ParticipantId : IEquatable<ParticipantId>
{
    private readonly Guid _value;

    private ParticipantId(Guid value)
    {
        _value = value;
    }

    public Guid Value()
    {
        return _value;
    }

    public static ParticipantId From(Guid value)
    {
        return new ParticipantId(value);
    }

    public bool Equals(ParticipantId? other)
    {
        if (other is null)
        {
            return false;
        }

        return _value.Equals(other._value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ParticipantId);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }
}
