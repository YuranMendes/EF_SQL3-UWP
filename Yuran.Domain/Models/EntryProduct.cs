using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class EntryProduct : Entity
    {
        public DateTime Date { get; set; }
        //public int UserId { get; set; }
        //public User User { get; set; }
        public int ProdutoId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }

    }
}
