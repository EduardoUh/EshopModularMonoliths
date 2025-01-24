namespace Shared.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        private readonly IMediator _mediator = mediator;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvents(DbContext? dbContext)
        {
            if (dbContext == null) return;

            // Getting aggregates that has at least one domain event
            var aggregates = dbContext.ChangeTracker
                .Entries<IAggregate>()
                .Where(aggregate => aggregate.Entity.DomainEvents.Any())
                .Select(aggregate => aggregate.Entity)
                .ToList();

            // Getting the domain events from the aggregates
            var domainEvents = aggregates
                .SelectMany(aggregate => aggregate.DomainEvents)
                .ToList();

            // Clear the domain events to avoid re-despaching events
            aggregates.ForEach(aggregate => aggregate.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
