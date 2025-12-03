namespace ExecutorBoard.Domain.Estates.ValueObjects;

public sealed class Update : IEquatable<Update>
{
    private readonly string _content;

    private Update(string content)
    {
        _content = content;
    }

    public static Update From(string content) => new(content);

    public bool Equals(Update? other)
    {
        if (other is null)
        {
            return false;
        }

        return string.Equals(_content, other._content, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Update);
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(_content);
    }
}
