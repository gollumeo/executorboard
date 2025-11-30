namespace EstateClear.Application.Commands;

public sealed class GrantParticipantAccessFlow(IEstates estates)
{
    public async Task Execute(GrantParticipantAccess input)
    {
        var estate = await estates.Load(input.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        estate.GrantParticipantAccess(input.Participant, input.Executor);

        await estates.Save(estate);
    }
}
