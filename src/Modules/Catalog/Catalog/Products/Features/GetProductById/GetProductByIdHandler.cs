namespace Catalog.Products.Features.GetProductById
{
    public record class GetProductByIdQuery(Guid Id)
        : IQuery<GetProductByIdResult>;

    public record class GetProductByIdResult(ProductDTO Product);

    public class GetProductByIdHandler(CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly CatalogDbContext _catalogDbContext = catalogDbContext;

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _catalogDbContext.Products
                                .AsNoTracking()
                                .FirstOrDefaultAsync(product => product.Id.Equals(request.Id), cancellationToken)
                                ?? throw new ProductNotFoundException(request.Id);

            return new GetProductByIdResult(product.Adapt<ProductDTO>());
        }
    }
}
