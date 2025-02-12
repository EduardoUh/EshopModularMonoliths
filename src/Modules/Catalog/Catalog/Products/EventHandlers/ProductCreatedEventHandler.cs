namespace Catalog.Products.EventHandlers
{
    public class ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
        : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger = logger;
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain event handled {DomainEvent}: Product {ProductName} with key [{Key}] was successfully created", notification.GetType().Name, notification.Product.Name, notification.Product.Id);

            return Task.CompletedTask;
        }
    }
}
