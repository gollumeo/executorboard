namespace ExecutorBoard.Persistence.EF.Records;

public sealed class EstateRecord
{
    public Guid Id { get; set; }

    public Guid ExecutorId { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
