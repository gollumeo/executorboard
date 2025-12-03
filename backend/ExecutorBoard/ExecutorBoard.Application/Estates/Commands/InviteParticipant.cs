namespace ExecutorBoard.Application.Estates.Commands;

public sealed class InviteParticipant(string email, Guid estateId, Guid executorId)
{
    public string Email { get; } = email;
    public Guid EstateId { get; } = estateId;
    public Guid ExecutorId { get; } = executorId;
}
