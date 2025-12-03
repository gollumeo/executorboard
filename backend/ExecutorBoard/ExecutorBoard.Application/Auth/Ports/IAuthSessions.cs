using ExecutorBoard.Domain.Auth.ValueObjects;

namespace ExecutorBoard.Application.Auth.Ports;

public interface IAuthSessions
{
    Task Add(SessionToken token, UserId userId);

    Task<bool> Exists(SessionToken token);

    Task Remove(SessionToken token);
}
