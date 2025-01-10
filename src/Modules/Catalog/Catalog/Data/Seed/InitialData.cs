namespace Catalog.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<Product> Products =>
        [
            Product.Create(new Guid(), "Iphone X", ["Category1"], "Long Description", "imageFile", 500),
            Product.Create(new Guid(), "Samsung 10", ["Category1"], "Long Description", "imageFile", 400),
            Product.Create(new Guid(), "Huawei Plus", ["Category2"], "Long Description", "imageFile", 650),
            Product.Create(new Guid(), "Xiaomi Mi", ["Category2"], "Long Description", "imageFile", 500)
        ];
    }
}
