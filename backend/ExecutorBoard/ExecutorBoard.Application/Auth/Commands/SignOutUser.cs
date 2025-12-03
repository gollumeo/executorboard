namespace ExecutorBoard.Application.Auth.Commands;

public sealed class SignOutUser(string sessionToken)
{
    public string SessionToken { get; } = sessionToken;
}
