namespace EstateClear.Domain.Estates.ValueObjects;

public sealed class ExecutorId(Guid value)
{
    private Guid _value { get; } = value;

    public Guid Value() => _value;

    public static ExecutorId From(Guid value) => new(value);
}
