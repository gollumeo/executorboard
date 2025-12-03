using ExecutorBoard.Application.Estates.Ports;
using ExecutorBoard.Domain.Estates.Entities;
using ExecutorBoard.Domain.Estates.ValueObjects;
using ExecutorBoard.Persistence.EF.Records;
using Microsoft.EntityFrameworkCore;

namespace ExecutorBoard.Persistence.EF.Repositories;

public sealed class EstatesRepositoryEf(ExecutorBoardDbContext context) : IEstates
{
    public async Task Add(EstateId estateId, ExecutorId executorId, string displayName)
    {
        var record = new EstateRecord
        {
            Id = estateId.Value(),
            ExecutorId = executorId.Value(),
            DisplayName = displayName,
            Status = EstateStatus.Active.ToString()
        };

        context.Estates.Add(record);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsWithName(ExecutorId executorId, EstateName estateName)
    {
        var normalizedName = estateName.Value();
        var exists = await context.Estates
            .AsNoTracking()
            .AnyAsync(estate =>
                estate.ExecutorId == executorId.Value() &&
                estate.DisplayName == normalizedName);

        return exists;
    }

    public async Task Rename(EstateId estateId, EstateName newName)
    {
        var record = await context.Estates.FirstAsync(estate => estate.Id == estateId.Value());
        record.DisplayName = newName.Value();
        await context.SaveChangesAsync();
    }

    public async Task<ExecutorId> Executor(EstateId estateId)
    {
        var executorId = await context.Estates
            .AsNoTracking()
            .Where(estate => estate.Id == estateId.Value())
            .Select(estate => estate.ExecutorId)
            .FirstAsync();

        return ExecutorId.From(executorId);
    }

    public async Task<EstateName> NameOf(EstateId estateId)
    {
        var name = await context.Estates
            .AsNoTracking()
            .Where(estate => estate.Id == estateId.Value())
            .Select(estate => estate.DisplayName)
            .FirstAsync();

        return EstateName.From(name);
    }

    public async Task Save(Estate estate)
    {
        var record = new EstateRecord
        {
            Id = estate.Id.Value(),
            ExecutorId = estate.ExecutorId.Value(),
            DisplayName = estate.DisplayName().Value(),
            Status = estate.Status.ToString()
        };

        var existing = await context.Estates.FirstOrDefaultAsync(e => e.Id == record.Id);

        if (existing is null)
        {
            context.Estates.Add(record);
        }
        else
        {
            existing.DisplayName = record.DisplayName;
            existing.Status = record.Status;
        }

        await context.SaveChangesAsync();
    }

    public async Task<Estate?> Load(EstateId estateId)
    {
        var record = await context.Estates
            .AsNoTracking()
            .FirstOrDefaultAsync(estate => estate.Id == estateId.Value());

        if (record is null)
        {
            return null;
        }

        var id = EstateId.From(record.Id);
        var executorId = ExecutorId.From(record.ExecutorId);
        var name = EstateName.From(record.DisplayName);
        var status = Enum.Parse<EstateStatus>(record.Status);

        return Estate.FromPersistence(id, executorId, name, status);
    }

    public async Task<IReadOnlyList<Estate>> ByExecutor(ExecutorId executorId)
    {
        var records = await context.Estates
            .AsNoTracking()
            .Where(estate => estate.ExecutorId == executorId.Value())
            .ToListAsync();

        var estates = records
            .Select(record =>
            {
                var id = EstateId.From(record.Id);
                var executor = ExecutorId.From(record.ExecutorId);
                var name = EstateName.From(record.DisplayName);
                var status = Enum.Parse<EstateStatus>(record.Status);
                return Estate.FromPersistence(id, executor, name, status);
            })
            .ToList()
            .AsReadOnly();

        return estates;
    }
}
