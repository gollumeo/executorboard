namespace EstateClear.Application;

public sealed class RevokeParticipantAccessFlow(IEstates estates)
{
    public async Task Execute(RevokeParticipantAccess input)
    {
        var estate = await estates.Load(input.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        throw new NotImplementedException();
    }
}
