using ExecutorBoard.Domain.Auth.ValueObjects;

namespace ExecutorBoard.Domain.Auth.Entities;

public sealed class User
{
    private User(UserId id, Email email, PasswordHash passwordHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
    }

    public UserId Id { get; }

    public Email Email { get; }

    public PasswordHash PasswordHash { get; }

    public static User Create(UserId id, Email email, PasswordHash passwordHash) => new(id, email, passwordHash);
}
