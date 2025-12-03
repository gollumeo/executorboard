namespace ExecutorBoard.Application.Auth.Commands;

public sealed class SignUpUser(string email, string passwordHash)
{
    public string Email { get; } = email;

    public string PasswordHash { get; } = passwordHash;
}
