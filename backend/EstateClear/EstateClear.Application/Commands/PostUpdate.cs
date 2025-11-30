using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Application.Commands;

public sealed class PostUpdate(EstateId estateId, Update update, Executor executor)
{
    public EstateId EstateId { get; } = estateId;
    public Update Update { get; } = update;
    public Executor Executor { get; } = executor;
}
