using CleanBooks.Domain.Entities.VolumeInfoData;
using CleanBooks.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CleanBooks.Infrastructure.Persistence.Configurations;

public class VolumeInfoConfiguration : IEntityTypeConfiguration<VolumeInfo>
{
    public void Configure(EntityTypeBuilder<VolumeInfo> builder)
    {
        builder.Property(t => t.Title)
            .IsRequired();

        builder.Property(q => q.PrintType)
            .HasConversion(v => v.ToString(),
                v => (PrintType)Enum.Parse(typeof(PrintType), v));

        var splitStringConverter = new ValueConverter<IList<string>, string>(
            v => string.Join(",", v), v => v.Split(new[] {','}));

        builder.Property(t => t.Authors)
            .HasConversion(splitStringConverter);

        builder.Property(t => t.Categories)
            .HasConversion(splitStringConverter);

        builder.OwnsOne(q=>q.ReadingModes);
        builder.OwnsOne(q=>q.PanelizationSummary);
        builder.OwnsOne(q=>q.ImageLinks);
        builder.OwnsOne(q=>q.Dimentions);

        builder.HasMany(q => q.IndustryIdentifiers).WithOne().HasForeignKey(q => q.VolumeInfoId);

        builder.HasIndex(q => q.Id);
        builder.HasIndex(q => q.Title);
        builder.HasIndex(q => q.Authors);
        builder.HasIndex(q => q.Categories);
    }
}