using System.Text.Json;
using DirectoryService.Domain.Constants;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeZone = DirectoryService.Domain.ValueObjects.TimeZone;

namespace DirectoryService.Infrastructure.Configurations;

public class LocationsConfiguration: IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations", "department");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(id => id.Id, value => LocationId.Create(value))
            .HasColumnName("location_id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(LocationNameConstants.MAX_LENGTH)
            .HasConversion(
                value => value.Value,
                value => LocationName.Create(value).Value)
            .HasColumnName("name");
        
        builder.Property(x => x.Addresses)
            .HasConversion(
                value => JsonSerializer.Serialize(value, JsonSerializerOptions.Default),
                valueDb =>
                    JsonSerializer.Deserialize<IReadOnlyList<Address>>(valueDb, JsonSerializerOptions.Default)!,
                new ValueComparer<IReadOnlyList<Address>>(
                    (c1, c2) => c1!.SequenceEqual(c2!),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()))
            .HasColumnName("addresses")
            .HasColumnType("jsonb");

        builder.Property(x => x.TimeZone)
            .IsRequired()
            .HasMaxLength(100) 
            .HasConversion(d =>  d.Value,
                v => TimeZone.Create(v).Value)
            .HasColumnName("timezone");
        
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