namespace EstateClear.Application.DTOs;

public sealed class SingleEstateProjection(Guid estateId, Guid executorId, string displayName, string status)
{
    public Guid EstateId { get; } = estateId;
    public Guid ExecutorId { get; } = executorId;
    public string DisplayName { get; } = displayName;
    public string Status { get; } = status;
}
