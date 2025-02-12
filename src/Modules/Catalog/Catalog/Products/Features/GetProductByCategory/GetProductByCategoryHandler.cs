namespace Catalog.Products.Features.GetProductByCategory
{
    public record class GetProductByCategoryQuery(string Category)
        : IQuery<GetProductByCategoryResult>;

    public record class GetProductByCategoryResult(IEnumerable<ProductDTO> Products);

    public class GetProductByCategoryHandler(CatalogDbContext catalogDbContext) :
        IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        private readonly CatalogDbContext _catalogDbContext = catalogDbContext;

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await _catalogDbContext.Products
                            .AsNoTracking()
                            .Where(product => product.Category.Contains(request.Category))
                            .OrderBy(product => product.Name)
                            .ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products.Adapt<IEnumerable<ProductDTO>>());
        }
    }
}
