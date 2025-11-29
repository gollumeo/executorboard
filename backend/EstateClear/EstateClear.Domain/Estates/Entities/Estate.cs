using EstateClear.Domain.Estates.ValueObjects;

namespace EstateClear.Domain.Estates.Entities;

public class Estate
{
    private Estate(EstateId id, ExecutorId executorId, EstateName displayName)
    {
        Id = id;
        ExecutorId = executorId;
        _displayName = displayName;
        _status = EstateStatus.Active;
    }

    private EstateName _displayName;
    private EstateStatus _status;
    private int _participantsCount;
    private readonly List<Participant> _participants = new();

    public EstateId Id { get; }

    public ExecutorId ExecutorId { get; }

    public EstateName DisplayName() => _displayName;

    public EstateStatus Status => _status;

    public void AddParticipant()
    {
        _participantsCount++;
    }

    public void AddParticipant(Participant participant)
    {
        foreach (var existing in _participants)
        {
            if (existing.Equals(participant))
            {
                throw new DomainException("Participant already exists");
            }
        }

        _participants.Add(participant);
    }

    public void RemoveParticipant()
    {
        if (_status == EstateStatus.Closed)
        {
            throw new DomainException("Estate is closed");
        }

        if (_participantsCount == 0)
        {
            return;
        }

        _participantsCount--;
    }

    public void RenameTo(EstateName newName)
    {
        if (_status == EstateStatus.Closed)
        {
            throw new DomainException("Estate is closed");
        }

        if (_participantsCount > 0)
        {
            throw new DomainException("Estate has participants");
        }

        _displayName = newName;
    }

    public void Close()
    {
        _participantsCount = 0;
        _status = EstateStatus.Closed;
    }

    public static Estate Create(EstateId id, ExecutorId executorId, EstateName estateName)
    {
        if (executorId is null)
        {
            throw new DomainException("Executor is required");
        }

        if (executorId.Value() == Guid.Empty)
        {
            throw new DomainException("Executor is required");
        }

        return new Estate(id, executorId, estateName);
    }
}
