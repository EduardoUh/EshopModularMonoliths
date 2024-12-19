namespace Catalog.Products.Models
{
    public class Product : Aggregate<Guid>
    {
        public string Name { get; private set; } = string.Empty;
        public List<string> Category { get; private set; } = [];
        public string Description { get; private set; } = string.Empty;
        public string ImageFile { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        public static Product Create(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var product = new Product
            {

                Id = id,
                Name = name,
                Category = category,
                Description = description,
                ImageFile = imageFile,
                Price = price
            };

            product.AddDomainEvent(new ProductCreatedEvent(product));

            return product;
        }

        public void Update(string name, List<string> category, string description, string imageFile, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            Name = name;
            Category = category;
            Description = description;
            ImageFile = imageFile;
            Price = price;

            // If price has changed, raise ProductPriceChanged domain event
            if (Price != price)
                AddDomainEvent(new ProductPriceChangedEvent(this));
        }
    }
}
// Add a Create method for initializing Product entities.
// Make property setters private to enforce encapsulation.
// Add an update method for modifying Product entities.
