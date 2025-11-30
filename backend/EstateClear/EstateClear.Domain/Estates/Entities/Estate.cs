using System.Collections;
using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Domain.Estates.Entities;

public class Estate
{
    private Estate(EstateId id, Executor executor, EstateName displayName)
    {
        Id = id;
        _executor = executor;
        _displayName = displayName;
        _status = EstateStatus.Active;
    }

    private EstateName _displayName;
    private EstateStatus _status;
    private readonly Executor _executor;
    private readonly List<Participant> _participants = new();
    private readonly IList _contributions = new ArrayList();
    private readonly IList _updates = new ArrayList();

    public EstateId Id { get; }

    public ExecutorId ExecutorId => ExecutorId.From(_executor.Id);

    public EstateName DisplayName() => _displayName;

    public EstateStatus Status => _status;

    public IReadOnlyList<Participant> Participants() => _participants.AsReadOnly();

    public void GrantParticipantAccess(Participant participant, Executor executor)
    {
        if (!executor.IsSame(_executor.Id))
        {
            throw new DomainException("Executor is required");
        }

        if (_participants.Any(existing => existing.Equals(participant)))
        {
            throw new DomainException("Participant already exists");
        }

        _participants.Add(participant);
    }

    public void RevokeParticipantAccess(Participant participant, Executor executor)
    {
        if (!executor.IsSame(_executor.Id))
        {
            throw new DomainException("Executor is required");
        }

        if (_contributions.Count > 0)
        {
            throw new DomainException("Cannot revoke participant with contributions");
        }

        var removed = _participants.Remove(participant);

        if (!removed)
        {
            throw new DomainException("Participant not found");
        }
    }

    public void PostUpdate(Update update, Executor executor)
    {
        if (_status == EstateStatus.Closed)
        {
            throw new DomainException("Estate is closed");
        }

        if (!executor.IsSame(_executor.Id))
        {
            throw new DomainException("Executor is required");
        }

        _updates.Add(update);
    }

    public void RenameTo(EstateName newName)
    {
        if (_status == EstateStatus.Closed)
        {
            throw new DomainException("Estate is closed");
        }

        if (_participants.Any())
        {
            throw new DomainException("Estate has participants");
        }

        _displayName = newName;
    }

    public void Close()
    {
        _participants.Clear();
        _status = EstateStatus.Closed;
    }

    public static Estate Create(EstateId id, ExecutorId executorId, EstateName estateName)
    {
        if (executorId.Value() == Guid.Empty)
        {
            throw new DomainException("Executor is required");
        }

        var executor = Executor.From(executorId.Value());

        return new Estate(id, executor, estateName);
    }
}