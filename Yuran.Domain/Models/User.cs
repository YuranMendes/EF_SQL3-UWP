using System.ComponentModel.DataAnnotations;
using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string PostalCodeId { get; set; }
        public PostalCode PostalCode { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        //public ICollection<EntryProduct> EntryProducts { get; set; }
        public ICollection<OutProduct> OutProducts { get; set; }

        public override string ToString()
        {
            return $"User Id: {Id}, Name: {Name}, PostalCode: {PostalCode}";
        }
    }

}



