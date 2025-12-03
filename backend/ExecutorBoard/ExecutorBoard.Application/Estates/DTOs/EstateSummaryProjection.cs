namespace ExecutorBoard.Application.Estates.DTOs;

public sealed class EstateSummaryProjection(Guid estateId, string displayName, string status)
{
    public Guid EstateId { get; } = estateId;
    public string DisplayName { get; } = displayName;
    public string Status { get; } = status;
}
