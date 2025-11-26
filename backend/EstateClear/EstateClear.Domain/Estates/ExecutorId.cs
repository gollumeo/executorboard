namespace EstateClear.Domain.Estates;

public sealed class ExecutorId(Guid value)
{
    public Guid Value { get; } = value;

    public static ExecutorId From(Guid value) => new(value);
}
