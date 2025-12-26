using System.Collections;
using ExecutorBoard.Domain.Estates.ValueObjects;

namespace ExecutorBoard.Domain.Estates.Entities;

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
    private readonly List<Participant> _participants = [];
    private readonly IList _contributions = new ArrayList();
    private readonly IList _updates = new ArrayList();

    public EstateId Id { get; }

    public ExecutorId ExecutorId => ExecutorId.From(_executor.Id);

    public EstateName DisplayName() => _displayName;

    public EstateStatus Status => _status;

    public IReadOnlyList<Participant> Participants() => _participants.AsReadOnly();

    public Participant AddPendingParticipant(string email)
    {
        var participant = Participant.Pending(email);
        _participants.Add(participant);
        return participant;
    }

    public Participant ActivateParticipant(ParticipantId id)
    {
        var existing = FindParticipant(id);
        var activated = existing.Activate();
        ReplaceParticipant(existing, activated);
        return activated;
    }

    public void GrantParticipantAccess(Participant participant, Executor executor)
    {
        EnsureExecutorAuthority(executor);
        EnsureParticipantDoesNotExist(participant);
        _participants.Add(participant);
    }

    public void RevokeParticipantAccess(Participant participant, Executor executor)
    {
        EnsureExecutorAuthority(executor);
        EnsureNoContributions();
        RemoveParticipant(participant);
    }

    public void PostUpdate(Update update, Executor executor)
    {
        EnsureOpen();
        EnsureExecutorAuthority(executor);
        _updates.Add(update);
    }

    public void RenameTo(EstateName newName)
    {
        EnsureOpen();
        EnsureNoParticipants();
        _displayName = newName;
    }

    public void Close()
    {
        RemoveAllParticipants();
        _status = EstateStatus.Closed;
    }

    public static Estate Create(EstateId id, ExecutorId executorId, EstateName estateName)
    {
        EnsureExecutorIdIsPresent(executorId);
        var executor = CreateExecutor(executorId);
        return new Estate(id, executor, estateName);
    }

    internal static Estate FromPersistence(EstateId id, ExecutorId executorId, EstateName name, EstateStatus status)
    {
        var executor = CreateExecutor(executorId);
        var estate = new Estate(id, executor, name)
        {
            _status = status
        };

        return estate;
    }

    private static void EnsureExecutorIdIsPresent(ExecutorId? executorId)
    {
        if (executorId is null || executorId.Value() == Guid.Empty)
        {
            throw new DomainException("Executor is required");
        }
    }

    private static Executor CreateExecutor(ExecutorId executorId)
    {
        return Executor.From(executorId.Value());
    }

    private void EnsureExecutorAuthority(Executor executor)
    {
        if (!executor.IsSame(_executor.Id))
        {
            throw new DomainException("Executor is required");
        }
    }

    private void EnsureParticipantDoesNotExist(Participant participant)
    {
        if (_participants.Any(existing => existing.Equals(participant)))
        {
            throw new DomainException("Participant already exists");
        }
    }

    private void EnsureNoContributions()
    {
        if (_contributions.Count > 0)
        {
            throw new DomainException("Cannot revoke participant with contributions");
        }
    }

    private void EnsureOpen()
    {
        if (_status == EstateStatus.Closed)
        {
            throw new DomainException("Estate is closed");
        }
    }

    private void EnsureNoParticipants()
    {
        if (_participants.Count != 0)
        {
            throw new DomainException("Estate has participants");
        }
    }

    private Participant FindParticipant(ParticipantId id)
    {
        return _participants.First(participant => participant.Id.Equals(id));
    }

    private void ReplaceParticipant(Participant existing, Participant updated)
    {
        var index = _participants.IndexOf(existing);
        _participants[index] = updated;
    }

    private void RemoveParticipant(Participant participant)
    {
        var removed = _participants.Remove(participant);
        EnsureParticipantRemoved(removed);
    }

    private static void EnsureParticipantRemoved(bool removed)
    {
        if (!removed)
        {
            throw new DomainException("Participant not found");
        }
    }

    private void RemoveAllParticipants()
    {
        _participants.Clear();
    }
}
