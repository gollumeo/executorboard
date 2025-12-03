namespace ExecutorBoard.Application.Estates.DTOs;

public sealed class EstateParticipantProjection(string email, string firstName, string lastName)
{
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
}
