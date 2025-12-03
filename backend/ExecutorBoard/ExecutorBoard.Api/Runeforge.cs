using Rituals.Contracts;
using Rituals.Runeforge;

namespace ExecutorBoard.Api;

public class Runeforge : DormantRuneforge
{
    protected override IEnumerable<IRune> Frostmourne()
    {
        yield break;
    }
}