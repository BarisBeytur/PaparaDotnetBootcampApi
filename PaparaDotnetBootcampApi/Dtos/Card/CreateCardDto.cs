using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaparaDotnetBootcampApi.Dtos.Card
{
    public class CreateCardDto
    {
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
