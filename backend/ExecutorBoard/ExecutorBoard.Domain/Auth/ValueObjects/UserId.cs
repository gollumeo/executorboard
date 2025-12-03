namespace ExecutorBoard.Domain.Auth.ValueObjects;

public sealed class UserId(Guid value)
{
    public Guid Value() => value;

    public static UserId From(Guid value) => new(value);
}
