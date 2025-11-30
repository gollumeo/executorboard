namespace EstateClear.Application.Commands;

public sealed class CreateEstate(Guid executorId, string displayName)
{
    public Guid ExecutorId { get; } = executorId;

    public string DisplayName { get; } = displayName;
}
