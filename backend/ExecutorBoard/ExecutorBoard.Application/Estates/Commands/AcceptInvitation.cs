namespace ExecutorBoard.Application.Estates.Commands;

public sealed class AcceptInvitation(string token)
{
    public string Token { get; } = token;
}
