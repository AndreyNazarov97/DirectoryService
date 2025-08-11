using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using DirectoryService.Domain.ValueObjects.EntityIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Path = DirectoryService.Domain.ValueObjects.Path;


namespace DirectoryService.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments", "department");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Id)
            .HasConversion(id => id.Id, value => DepartmentId.Create(value))
            .HasColumnName("department_id");


        builder.Property(d => d.Name)
            .HasConversion(name => name.Value, value => DepartmentName.Create(value).Value)
            .HasMaxLength(150)
            .IsRequired()
            .HasColumnName("name");

        builder.Property(d => d.Identifier)
            .HasConversion(i => i.Value, v => Identifier.Create(v).Value)
            .HasMaxLength(150)
            .IsRequired()
            .HasColumnName("identifier");

        builder.Property(d => d.Path)
            .HasConversion(p => p.Value, v => Path.Create(v).Value)
            .IsRequired()
            .HasColumnName("path");

        builder.Property(d => d.Depth)
            .HasConversion(d => d.Value, v => Depth.Create(v).Value)
            .IsRequired()
            .HasColumnName("depth");

        builder.Property(d => d.ChildrenCount)
            .HasConversion(c => c.Value, v => ChildrenCount.Create(v).Value)
            .IsRequired()
            .HasColumnName("children_count");


        builder.Property(d => d.IsDeleted)
            .IsRequired()
            .HasColumnName("is_deleted"); 

        builder.Property(d => d.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder.Property(d => d.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at");
        
        builder.Property(d => d.DeletionDate)
            .HasColumnName("deletion_date");
        
        builder.Property(d => d.ParentId)
            .HasConversion(
                id => id == null ? null :  (Guid?)id.Id, 
                value =>  value.HasValue ? DepartmentId.Create(value.Value) : null)
            .HasColumnName("parent_id");
        
        var departmentIdComparer = new ValueComparer<DepartmentId>(
            (c1, c2) => c1.Equals(c2),
            c => c.GetHashCode());
        
        builder.Property(d => d.ParentId).Metadata.SetValueComparer(departmentIdComparer);
        
        
        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(d => d.ParentId)
            .IsRequired(false);

        // Связи многие-ко-многим
        builder.HasMany(d => d.Locations)
            .WithMany(l => l.Departments)
            .UsingEntity(
                "department_locations",
                l => l.HasOne(typeof(Location)).WithMany().HasForeignKey("location_id"),
                r => r.HasOne(typeof(Department)).WithMany().HasForeignKey("department_id"),
                j => j.HasKey("department_id", "location_id"));

        builder.HasMany(d => d.Positions)
            .WithMany(p => p.Departments)
            .UsingEntity(
                "department_positions",
                l => l.HasOne(typeof(Position)).WithMany().HasForeignKey("position_id"),
                r => r.HasOne(typeof(Department)).WithMany().HasForeignKey("department_id"),
                j => j.HasKey("department_id", "position_id"));
    }
    
}