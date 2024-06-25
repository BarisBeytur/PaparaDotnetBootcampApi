using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaparaDotnetBootcampApi.Models
{
    public class Card : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public string ExpiryDate { get; set; }
        [Required]
        public string Cvv { get; set; }

        [Required]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
