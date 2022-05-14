
using Microsoft.EntityFrameworkCore;

namespace CleanBooks.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
