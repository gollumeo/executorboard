namespace ExecutorBoard.Persistence.EF.Records;

public sealed class UserRecord
{
    public Guid Id { get; set; }

    public string Email { get; init; } = string.Empty;

    public string PasswordHash { get; init; } = string.Empty;
}
