using ExecutorBoard.Application.Estates.Commands;
using ExecutorBoard.Application.Estates.Ports;

namespace ExecutorBoard.Application.Estates.Flows;

public sealed class PostUpdateFlow(IEstates estates)
{
    public async Task Execute(PostUpdate input)
    {
        var estate = await estates.Load(input.EstateId);
        if (estate is null)
        {
            throw new Exception("Estate not found");
        }

        estate.PostUpdate(input.Update, input.Executor);

        await estates.Save(estate);

    }
}
