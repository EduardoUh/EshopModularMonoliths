namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductHandler(CatalogDbContext catalogDbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly CatalogDbContext _catalogDbContext = catalogDbContext;

        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _catalogDbContext.Products.FindAsync([request.Id], cancellationToken);

            if (product is null) throw new Exception($"Product with the key [{request.Id}] not found");

            _catalogDbContext.Products.Remove(product);

            await _catalogDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
