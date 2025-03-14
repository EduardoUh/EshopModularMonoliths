namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDTO Product) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Product.Id)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Product.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }

    public class UpdateProductHandler(CatalogDbContext catalogDbContext)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly CatalogDbContext _catalogDbContext = catalogDbContext;

        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _catalogDbContext.Products.FindAsync([request.Product.Id], cancellationToken)
                ?? throw new ProductNotFoundException(request.Product.Id);

            UpdateProductWithNewValues(product, request.Product);

            _catalogDbContext.Products.Update(product);

            await _catalogDbContext.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }

        private static void UpdateProductWithNewValues(Product product, ProductDTO productDTO)
        {
            product.Update(
                productDTO.Name,
                productDTO.Category,
                productDTO.Description,
                productDTO.ImageFile,
                productDTO.Price
                );
        }
    }
}
