using DirectoryService.Domain.Constants;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions", "department");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(id => id.Id, value => PositionId.Create(value))
            .HasColumnName("position_id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(PositionNameConstants.MAX_LENGTH)
            .HasConversion(
                value => value.Value,
                value => PositionName.Create(value).Value)
            .HasColumnName("name");

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(DescriptionConstants.MAX_LENGTH)
            .HasConversion(d => d == null ? null : d.Value,
                v => v == null ? null : Description.Create(v).Value)
            .HasColumnName("description");
        
        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted");

        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(p => p.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");
        
        builder.Property(p => p.DeletionDate)
            .HasColumnName("deletion_date");
    }
}