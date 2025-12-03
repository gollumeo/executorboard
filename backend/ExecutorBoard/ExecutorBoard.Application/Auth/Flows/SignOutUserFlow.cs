using ExecutorBoard.Application.Auth.Commands;
using ExecutorBoard.Application.Auth.Ports;
using ExecutorBoard.Domain.Auth.ValueObjects;

namespace ExecutorBoard.Application.Auth.Flows;

public sealed class SignOutUserFlow(IAuthSessions sessions)
{
    public async Task Execute(SignOutUser input)
    {
        var token = SessionToken.From(input.SessionToken);
        await sessions.Remove(token);
    }
}
