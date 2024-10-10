using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class Product : Entity
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<EntryProduct>? EntryProducts { get; set; }
        public ICollection<OutProduct>? OutProducts { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Price: {Price}";
        }
    }

}
