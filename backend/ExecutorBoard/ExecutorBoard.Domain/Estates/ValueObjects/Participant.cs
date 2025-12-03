namespace ExecutorBoard.Domain.Estates.ValueObjects;

public sealed class Participant : IEquatable<Participant>
{
    private readonly string _email;
    private readonly string? _firstName;
    private readonly string? _lastName;

    private Participant(ParticipantId id, string email, string? firstName, string? lastName, ParticipantStatus status)
    {
        Id = id;
        _email = email.Trim().ToLowerInvariant();
        _firstName = firstName;
        _lastName = lastName;
        Status = status;
    }

    public ParticipantId Id { get; }

    public ParticipantStatus Status { get; }

    public string Email() => _email;

    public string? FirstName() => _firstName;

    public string? LastName() => _lastName;

    public static Participant From(string email, string? firstName, string? lastName, ParticipantStatus status = ParticipantStatus.Active)
    {
        var id = ParticipantId.From(Guid.NewGuid());
        return new Participant(id, email, firstName, lastName, status);
    }

    public static Participant Pending(string email)
    {
        var id = ParticipantId.From(Guid.NewGuid());
        return new Participant(id, email, null, null, ParticipantStatus.Pending);
    }

    public Participant Activate()
    {
        return new Participant(Id, _email, _firstName, _lastName, ParticipantStatus.Active);
    }

    public bool Equals(Participant? other)
    {
        if (other is null)
        {
            return false;
        }

        return string.Equals(_email, other._email, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Participant);
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(_email);
    }
}
