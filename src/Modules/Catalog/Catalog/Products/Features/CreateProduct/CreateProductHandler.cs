namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(ProductDTO Product) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Product.Category)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Product.ImageFile)
                .NotEmpty().WithMessage("{PropertyName} is required");

            RuleFor(x => x.Product.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
        }
    }

    public class CreateProductHandler(
        CatalogDbContext catalogDbContext,
        IValidator<CreateProductCommand> validator,
        ILogger<CreateProductHandler> logger
        )
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly CatalogDbContext _catalogDbContext = catalogDbContext;
        private readonly IValidator<CreateProductCommand> _validator = validator;
        private readonly ILogger<CreateProductHandler> _logger = logger;

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);

            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }

            _logger.LogInformation("{@name}.Handle called with {@request}", nameof(CreateProductHandler), request);

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
