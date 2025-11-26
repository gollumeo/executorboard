namespace EstateClear.Domain.Estates;

public sealed class EstateId(Guid value)
{
    public Guid Value { get; } = value;

    public static EstateId From(Guid value) => new(value);
}
