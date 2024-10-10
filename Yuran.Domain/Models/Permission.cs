using Yuran.Domain.SeedWork;

namespace Yuran.Domain.Models
{
    public class Permission : Entity
    {
        public string? Name { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public override string ToString()
        {
            return Name!;
        }



    }
}
