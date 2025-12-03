using ExecutorBoard.Persistence.EF.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExecutorBoard.Persistence.EF.Configurations;

public sealed class EstateRecordConfiguration : IEntityTypeConfiguration<EstateRecord>
{
    public void Configure(EntityTypeBuilder<EstateRecord> builder)
    {
        builder.ToTable("Estates");

        builder.HasKey(estate => estate.Id);

        builder.Property(estate => estate.ExecutorId)
            .IsRequired();

        builder.Property(estate => estate.DisplayName)
            .IsRequired();

        builder.Property(estate => estate.Status)
            .IsRequired();
    }
}
