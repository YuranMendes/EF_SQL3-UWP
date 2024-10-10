using Yuran.Domain.SeedWork;
namespace Yuran.Domain.Models
{
    public class Fornecedor : Entity
    {
        public string? Name { get; set; }
        public string? Telefone { get; set; }
        public string? PostalCodeId { get; set; }
        public PostalCode? PostalCode { get; set; }
        public ICollection<EntryProduct>? EntryProducts { get; set; }

        public override string ToString()
        {
            return $"Fornecedor Id: {Id}, Name: {Name}, Telefone: {Telefone}, PostalCodeId: {PostalCode}";
        }
    }

}
