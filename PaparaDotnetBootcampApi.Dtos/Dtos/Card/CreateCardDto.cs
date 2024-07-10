using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Card
{
    /// <summary>
    /// bu sınıf, kart oluşturmak için kullanılır.
    /// </summary>
    public class CreateCardDto
    {
        public string CardNumber { get; set; }
        public string NameSurname { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }

        public int CustomerId { get; set; }
    }
}
