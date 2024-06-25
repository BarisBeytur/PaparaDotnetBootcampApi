using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaparaDotnetBootcampApi.Models
{
    public class Customer : IEntity
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string TCKN { get; set; }

        public ICollection<Card>? Cards { get; set; } = new List<Card>();
    }
}
