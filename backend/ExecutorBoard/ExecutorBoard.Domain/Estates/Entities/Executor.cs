namespace ExecutorBoard.Domain.Estates.ValueObjects;

public class Executor
{
    private Executor(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public bool IsSame(Guid id) => Id == id;

    public static Executor From(Guid id) => new(id);
}
