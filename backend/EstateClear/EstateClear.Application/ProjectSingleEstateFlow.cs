using System.Threading.Tasks;

namespace EstateClear.Application;

public sealed class ProjectSingleEstateFlow(IEstates estates)
{
    public async Task<DTOs.SingleEstateProjection?> Execute(ProjectSingleEstate input)
    {
        var estate = await estates.Load(input.EstateId);

        if (estate is null)
        {
            return null;
        }

        return new DTOs.SingleEstateProjection(
            estate.Id.Value(),
            estate.ExecutorId.Value(),
            estate.DisplayName().Value(),
            estate.Status.ToString());
    }
}
