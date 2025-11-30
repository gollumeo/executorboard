namespace EstateClear.Application.Commands;

public sealed class RevokeParticipantAccessFlow(IEstates estates)
{
    public async Task Execute(RevokeParticipantAccess input)
    {
        var estate = await estates.Load(input.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        estate.RevokeParticipantAccess(input.Participant, input.Executor);

        await estates.Save(estate);
    }
}
