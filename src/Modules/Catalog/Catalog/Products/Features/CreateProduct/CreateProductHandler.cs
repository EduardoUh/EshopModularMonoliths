namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDTO Product) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductHandler(CatalogDbContext catalogDbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly CatalogDbContext _catalogDbContext = catalogDbContext;

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = CreateNewProduct(request.Product);

            _catalogDbContext.Products.Add(product);

            await _catalogDbContext.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }

        private static Product CreateNewProduct(ProductDTO productDTO)
        {
            return Product.Create(
                Guid.NewGuid(),
                productDTO.Name,
                productDTO.Category,
                productDTO.Description,
                productDTO.ImageFile,
                productDTO.Price
                );
        }
    }
}
