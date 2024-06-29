using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Models
{
    public class Card : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string NameSurname { get; set; }

        [Required]
        public string ExpiryDate { get; set; }

        [Required]
        public string Cvv { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
