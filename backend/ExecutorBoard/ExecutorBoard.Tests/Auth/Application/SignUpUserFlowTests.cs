using ExecutorBoard.Application.Auth.Commands;
using ExecutorBoard.Application.Auth.Flows;
using ExecutorBoard.Domain.Auth.ValueObjects;
using ExecutorBoard.Tests.Auth.Fakes;

namespace ExecutorBoard.Tests.Auth.Application;

public class SignUpUserFlowTests
{
    [Fact]
    public async Task SigningUpWithValidEmailAndPasswordHashCreatesUser()
    {
        var input = new SignUpUser("  USER@example.com  ", "hashed-password");
        var users = new UsersFake();
        var flow = new SignUpUserFlow(users);

        var result = await flow.Execute(input);

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.UserId.Value());
        Assert.Single(users.AddedUsers);
        var added = users.AddedUsers.Single();
        Assert.Equal(result.UserId.Value(), added.UserId.Value());
        Assert.Equal("user@example.com", added.Email.Value());
        Assert.Equal("hashed-password", added.PasswordHash.Value());
    }

    [Fact]
    public async Task SigningUpWithExistingEmailShouldThrow()
    {
        var email = Email.From("existing@example.com");
        var passwordHash = PasswordHash.From("hashed-password");
        var users = new UsersFake();
        users.ExistingUsers[email] = (UserId.From(Guid.NewGuid()), passwordHash);
        var input = new SignUpUser("existing@example.com", "hashed-password");
        var flow = new SignUpUserFlow(users);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Single(users.ExistingUsers);
        Assert.Empty(users.AddedUsers);
    }

    [Fact]
    public async Task SigningUpWithInvalidEmailShouldThrow()
    {
        var input = new SignUpUser("invalid-email", "hashed-password");
        var users = new UsersFake();
        var flow = new SignUpUserFlow(users);

        var action = () => flow.Execute(input);

        await Assert.ThrowsAnyAsync<Exception>(action);
        Assert.Empty(users.AddedUsers);
    }
}
