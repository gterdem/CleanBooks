using CleanBooks.Domain.Common;

namespace CleanBooks.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
