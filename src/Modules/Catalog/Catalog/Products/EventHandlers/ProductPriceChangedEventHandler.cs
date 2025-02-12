namespace Catalog.Products.EventHandlers
{
    public class ProductPriceChangedEventHandler(ILogger<ProductPriceChangedEventHandler> logger)
        : INotificationHandler<ProductPriceChangedEvent>
    {
        private readonly ILogger<ProductPriceChangedEventHandler> _logger = logger;

        public Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
        {
            // TODO: Publish product price changed integration event for updating basket prices

            _logger.LogInformation("Domain event handled {DomainEvent}: Price of product {ProductName} successfully updated", notification.GetType().Name, notification.Product.Name);

            return Task.CompletedTask;
        }
    }
}
