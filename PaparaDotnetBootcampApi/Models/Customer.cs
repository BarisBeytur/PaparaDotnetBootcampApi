using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Models
{
    public class Customer : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string TCKN { get; set; }

        public virtual ICollection<Card>? Cards { get; set; } = new List<Card>();
    }
}
