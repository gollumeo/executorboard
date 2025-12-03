using System;

namespace ExecutorBoard.Domain.Estates.ValueObjects;

public sealed class ExecutorId(Guid value) : IEquatable<ExecutorId>
{
    private Guid _value { get; } = value;

    public Guid Value() => _value;

    public bool Equals(ExecutorId? other)
    {
        if (other is null)
        {
            return false;
        }

        return _value.Equals(other._value);
    }

    public override bool Equals(object? obj) => Equals(obj as ExecutorId);

    public override int GetHashCode() => _value.GetHashCode();

    public static ExecutorId From(Guid value) => new(value);
}
