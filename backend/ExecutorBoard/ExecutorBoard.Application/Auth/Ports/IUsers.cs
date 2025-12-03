using ExecutorBoard.Domain.Auth.Entities;
using ExecutorBoard.Domain.Auth.ValueObjects;

namespace ExecutorBoard.Application.Auth.Ports;

public interface IUsers
{
    Task<bool> ExistsByEmail(Email email);

    Task<User?> FindByEmail(Email email);

    Task Add(User user);
}
