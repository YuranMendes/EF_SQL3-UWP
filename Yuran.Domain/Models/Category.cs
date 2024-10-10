using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }

        public Category()
        {
            Name = "";
            Description = "";
            Products = new List<Product>(); // Initialize the collection in the constructor;
        }
                

        public override string ToString()
        {
            return Name;
        }

    }
}
