using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class OutProduct : Entity
    {
        public DateTime? Date { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int DestinoId { get; set; }
        public Destino? Destino { get; set; }


    }
}
