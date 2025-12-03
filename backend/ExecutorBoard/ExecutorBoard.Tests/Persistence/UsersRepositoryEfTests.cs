using ExecutorBoard.Application.Auth.Ports;
using ExecutorBoard.Domain.Auth.Entities;
using ExecutorBoard.Domain.Auth.ValueObjects;
using ExecutorBoard.Persistence.EF;
using ExecutorBoard.Persistence.EF.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ExecutorBoard.Tests.Persistence;

public class UsersRepositoryEfTests
{
    [Fact]
    public async Task Add_ShouldPersist_And_FindByEmail_ReturnsUser()
    {
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<ExecutorBoardDbContext>()
            .UseSqlite(connection)
            .Options;

        await using (var context = new ExecutorBoardDbContext(options))
        {
            context.Database.EnsureCreated();
            IUsers repository = new UsersRepositoryEf(context);
            var user = User.Create(UserId.From(Guid.NewGuid()), Email.From("user@example.com"), PasswordHash.From("hashed-password"));

            await repository.Add(user);
        }

        await using var verificationContext = new ExecutorBoardDbContext(options);
        IUsers verificationRepository = new UsersRepositoryEf(verificationContext);

        var found = await verificationRepository.FindByEmail(Email.From("user@example.com"));

        Assert.NotNull(found);
        Assert.Equal("user@example.com", found!.Email.Value());
        Assert.Equal("hashed-password", found.PasswordHash.Value());
    }

    [Fact]
    public async Task ExistsByEmail_ShouldReturnTrue_WhenExists()
    {
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<ExecutorBoardDbContext>()
            .UseSqlite(connection)
            .Options;

        await using (var context = new ExecutorBoardDbContext(options))
        {
            context.Database.EnsureCreated();
            IUsers repository = new UsersRepositoryEf(context);
            var user = User.Create(UserId.From(Guid.NewGuid()), Email.From("user@example.com"), PasswordHash.From("hashed-password"));

            await repository.Add(user);
        }

        await using var verificationContext = new ExecutorBoardDbContext(options);
        IUsers verificationRepository = new UsersRepositoryEf(verificationContext);

        var exists = await verificationRepository.ExistsByEmail(Email.From("user@example.com"));

        Assert.True(exists);
    }

    [Fact]
    public async Task FindByEmail_ShouldReturnNull_WhenMissing()
    {
        using var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<ExecutorBoardDbContext>()
            .UseSqlite(connection)
            .Options;

        await using var context = new ExecutorBoardDbContext(options);
        context.Database.EnsureCreated();
        IUsers repository = new UsersRepositoryEf(context);

        var found = await repository.FindByEmail(Email.From("missing@example.com"));

        Assert.Null(found);
    }
}
