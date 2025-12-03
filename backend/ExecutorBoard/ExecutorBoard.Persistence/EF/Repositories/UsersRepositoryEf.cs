using ExecutorBoard.Application.Auth.Ports;
using ExecutorBoard.Domain.Auth.Entities;
using ExecutorBoard.Domain.Auth.ValueObjects;
using ExecutorBoard.Persistence.EF.Records;
using Microsoft.EntityFrameworkCore;

namespace ExecutorBoard.Persistence.EF.Repositories;

public sealed class UsersRepositoryEf(ExecutorBoardDbContext context) : IUsers
{
    public async Task Add(User user)
    {
        var record = new UserRecord
        {
            Id = user.Id.Value(),
            Email = user.Email.Value(),
            PasswordHash = user.PasswordHash.Value()
        };

        context.Users.Add(record);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByEmail(Email email)
    {
        var normalizedEmail = email.Value();
        return await context.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email == normalizedEmail);
    }

    public async Task<User?> FindByEmail(Email email)
    {
        var normalizedEmail = email.Value();
        var record = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == normalizedEmail);

        if (record is null)
        {
            return null;
        }

        var userId = UserId.From(record.Id);
        var userEmail = Email.From(record.Email);
        var passwordHash = PasswordHash.From(record.PasswordHash);

        return User.Create(userId, userEmail, passwordHash);
    }
}
