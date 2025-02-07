namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery() : IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<ProductDTO> Products);

    public class GetProductsHandler(CatalogDbContext catalogDbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        private readonly CatalogDbContext _catalogDbContext = catalogDbContext;

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _catalogDbContext.Products
                .AsNoTracking()
                .OrderBy(product => product.Name)
                .ToListAsync(cancellationToken);

            var productsDTOs = products.Adapt<IEnumerable<ProductDTO>>();

            return new GetProductsResult(productsDTOs);
        }
    }
}
