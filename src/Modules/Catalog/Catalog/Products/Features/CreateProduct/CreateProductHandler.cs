namespace Catalog.Products.Features.CreateProduct
{
    public record CreateProductCommand(
        string Name,
        ICollection<string> Category,
        string Description,
        string ImageFile,
        decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
