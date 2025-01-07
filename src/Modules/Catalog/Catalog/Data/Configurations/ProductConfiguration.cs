﻿namespace Catalog.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);

            builder.Property(product => product.Name).HasMaxLength(70).IsRequired();

            builder.Property(product => product.Category).IsRequired();

            builder.Property(product => product.Description).HasMaxLength(200);

            builder.Property(product => product.ImageFile).HasMaxLength(150);

            builder.Property(product => product.Price).IsRequired();

            builder.Ignore(product => product.DomainEvents);
        }
    }
}