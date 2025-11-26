namespace EstateClear.Domain.Estates;

public sealed class EstateId(Guid value)
{
    private Guid _value { get; } = value;

    public Guid Value() => _value;

    public static EstateId From(Guid value) => new(value);
}
