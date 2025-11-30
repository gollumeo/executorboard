using EstateClear.Application.DTOs;

namespace EstateClear.Application.Queries;

public sealed class ProjectParticipantsOfEstateFlow(IEstates estates)
{
    public async Task<IReadOnlyList<EstateParticipantProjection>> Execute(ProjectParticipantsOfEstate input)
    {
        var estate = await estates.Load(input.EstateId);

        if (estate is null)
        {
            return [];
        }

        var projections = estate
            .Participants()
            .Select(participant => new EstateParticipantProjection(
                participant.Email(),
                participant.FirstName() ?? string.Empty,
                participant.LastName() ?? string.Empty))
            .ToList()
            .AsReadOnly();

        return projections;
    }
}
