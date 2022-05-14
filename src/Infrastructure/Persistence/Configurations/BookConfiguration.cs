using CleanBooks.Domain.Entities;
using CleanBooks.Domain.Entities.VolumeInfoData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanBooks.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasOne(q => q.VolumeInfo)
            .WithOne().HasForeignKey<VolumeInfo>(t => t.BookId);

        builder.HasIndex(q => q.Id);
    }
}