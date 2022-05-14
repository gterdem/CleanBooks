using CleanBooks.Application.Common.Interfaces;

namespace CleanBooks.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
