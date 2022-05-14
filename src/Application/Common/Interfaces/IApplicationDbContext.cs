using CleanBooks.Domain.Entities;
using CleanBooks.Domain.Entities.VolumeInfoData;
using Microsoft.EntityFrameworkCore;

namespace CleanBooks.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<VolumeInfo> VolumeInfo { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}