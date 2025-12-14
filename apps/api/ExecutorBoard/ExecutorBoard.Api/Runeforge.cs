using Rituals.Contracts;
using Rituals.Runeforge;
using ExecutorBoard.Api.System.Bootstrap;

namespace ExecutorBoard.Api;

public class Runeforge : DormantRuneforge
{
    protected override IEnumerable<IRune> Frostmourne()
    {
        yield return new SystemBootstrapRune();
    }
}
