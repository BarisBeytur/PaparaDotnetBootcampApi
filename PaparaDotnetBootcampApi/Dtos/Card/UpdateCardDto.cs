using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaparaDotnetBootcampApi.Dtos.Card
{
    public class UpdateCardDto
    {
        [Required]
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

    }
}
