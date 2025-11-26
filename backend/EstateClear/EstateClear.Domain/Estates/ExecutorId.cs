namespace EstateClear.Domain.Estates;

public sealed class ExecutorId(Guid value)
{
    private Guid _value { get; } = value;

    public Guid Value() => _value;

    public static ExecutorId From(Guid value) => new(value);
}
