using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaparaDotnetBootcampApi.Dtos.Card
{
    /// <summary>
    /// bu sınıf, kart güncelleme işlemleri için kullanılır.
    /// </summary>
    public class UpdateCardDto
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string NameSurname { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }

        public int CustomerId { get; set; }

    }
}
