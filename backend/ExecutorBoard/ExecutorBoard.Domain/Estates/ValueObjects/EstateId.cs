namespace ExecutorBoard.Domain.Estates.ValueObjects;

public sealed class EstateId(Guid value) : IEquatable<EstateId>
{
    private Guid _value { get; } = value;

    public Guid Value() => _value;

    public bool Equals(EstateId? other)
    {
        if (other is null)
        {
            return false;
        }

        return _value.Equals(other._value);
    }

    public override bool Equals(object? obj) => Equals(obj as EstateId);

    public override int GetHashCode() => _value.GetHashCode();

    public static EstateId From(Guid value) => new(value);
}
