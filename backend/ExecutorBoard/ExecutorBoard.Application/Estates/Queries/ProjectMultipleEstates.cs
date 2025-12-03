using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Application.Estates.Queries;

public sealed class ProjectMultipleEstates(ExecutorId executorId)
{
    public ExecutorId ExecutorId { get; } = executorId;
}
