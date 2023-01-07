using System.Data;
using FluentValidation;

namespace clouddb_sdv_2022_fa.Modules.Orders
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string imageBlob { get; set; }
        public virtual List<Review> Reviews { get; set; }

        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public class ProductValidator : AbstractValidator<Product>
        {
            public ProductValidator()
            {
                RuleFor(p => p.Name).Length(0, 30).NotEmpty();
                RuleFor(p => p.Description).Length(0, 1000).NotEmpty();
                RuleFor(p => p.Price).GreaterThan(0);
                RuleFor(p => p.imageBlob).NotEmpty();
            }
        }
    }
}