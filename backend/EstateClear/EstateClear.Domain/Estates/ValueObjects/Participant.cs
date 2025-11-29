namespace EstateClear.Domain.Estates.ValueObjects;

public sealed class Participant : IEquatable<Participant>
{
    private readonly string _email;
    private readonly string? _firstName;
    private readonly string? _lastName;

    private Participant(string email, string? firstName, string? lastName)
    {
        _email = email;
        _firstName = firstName;
        _lastName = lastName;
    }

    public static Participant From(string email, string? firstName, string? lastName)
    {
        return new Participant(email, firstName, lastName);
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
