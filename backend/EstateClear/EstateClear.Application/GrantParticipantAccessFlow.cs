namespace EstateClear.Application;

public sealed class GrantParticipantAccessFlow(IEstates estates)
{
    public async Task Execute(GrantParticipantAccess input)
    {
        var estate = await estates.Load(input.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        throw new NotImplementedException();
    }
}
