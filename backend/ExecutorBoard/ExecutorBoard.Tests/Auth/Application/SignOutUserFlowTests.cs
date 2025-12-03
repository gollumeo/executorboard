using ExecutorBoard.Application.Auth.Commands;
using ExecutorBoard.Application.Auth.Flows;
using ExecutorBoard.Domain.Auth.ValueObjects;
using ExecutorBoard.Tests.Auth.Fakes;

namespace ExecutorBoard.Tests.Auth.Application;

public class SignOutUserFlowTests
{
    [Fact]
    public async Task SignOutShouldInvalidateExistingSession()
    {
        var email = Email.From("user@example.com");
        var passwordHash = PasswordHash.From("hashed-password");
        var users = new UsersFake();
        var userId = UserId.From(Guid.NewGuid());
        users.ExistingUsers[email] = (userId, passwordHash);
        var signInFlow = new SignInUserFlow(users);
        var signIn = new SignInUser("user@example.com", "hashed-password");
        var signedIn = await signInFlow.Execute(signIn);
        var sessionToken = signedIn.SessionToken;
        var sessions = new AuthSessionsFake();
        await sessions.Add(sessionToken, userId);
        var signOut = new SignOutUser(sessionToken.Value());
        var signOutFlow = new SignOutUserFlow(sessions);

        await signOutFlow.Execute(signOut);

        var exists = await sessions.Exists(sessionToken);
        Assert.False(exists);
    }
}
