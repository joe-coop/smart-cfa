using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Smart.FA.Catalog.Core.Domain;

namespace Smart.FA.Catalog.Infrastructure.Persistence.Configuration;

public class TrainingTargetConfigurations: IEntityTypeConfiguration<TrainingTarget>
{
    public void Configure(EntityTypeBuilder<TrainingTarget> builder)
    {
        builder.HasKey(target => new {target.TrainingId, target.TrainingTargetAudienceId});
        builder.HasOne(detail => detail.Training)
            .WithMany(training => training.Targets)
            .HasForeignKey(detail => detail.TrainingId);

        builder.ToTable("TrainingTarget");
    }
}
