using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Card
{
    /// <summary>
    /// bu sınıf, kart oluşturmak için kullanılır.
    /// </summary>
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
