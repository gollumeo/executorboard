namespace EstateClear.Application.Commands;

public sealed class CloseEstateFlow(IEstates estates)
{
    public async Task Execute(CloseEstate input)
    {
        var estate = await estates.Load(input.EstateId);

        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        estate.Close();

        await estates.Save(estate);
    }
}
