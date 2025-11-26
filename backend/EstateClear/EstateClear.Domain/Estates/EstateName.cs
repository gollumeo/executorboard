using System.Linq;

namespace EstateClear.Domain.Estates;

public sealed class EstateName
{
    private EstateName(string value)
    {
        _value = value;
    }

    private string _value { get; }

    public string Value() => _value;

    public static EstateName From(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new DomainException("Display name is required");
        }

        var trimmedDisplayName = displayName.Trim();

        if (trimmedDisplayName.Length < 2)
        {
            throw new DomainException("Display name is too short");
        }

        var normalized = string.Join(
            " ",
            trimmedDisplayName
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(word =>
                {
                    var lower = word.ToLowerInvariant();
                    return char.ToUpperInvariant(lower[0]) + lower[1..];
                }));

        return new EstateName(normalized);
    }
}
