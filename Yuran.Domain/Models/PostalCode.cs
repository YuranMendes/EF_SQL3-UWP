using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class PostalCode : Entity
    {
        public new string? Id { get; set; }
        public string? Localidade { get; set; }
        public ICollection<User>? Users { get; set; }
        public ICollection<Destino>? Destinos { get; set; }
        public ICollection<Fornecedor>? Fornecedores { get; set; }
        // Override para retornar logo o Nome
        public override string ToString()
        {
            return $"{Id}, Localidade: {Localidade}";
        }

    }
}
