using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class Destino : Entity
    {

        public string Description { get; set; }

        public string PostalCodeId { get; set; }

        public PostalCode? PostalCode { get; set; }
        public ICollection<OutProduct> OutProducts { get; set; }

        public Destino()
        {
            Description = "UNAVAIABLE";
            PostalCodeId = "UNAVAIABLE";
            PostalCode = null;
            OutProducts = new List<OutProduct>(); // Initialize the collection in the constructor;
        }

        public override string ToString()
        {
            return Description;
        }


    }
}
