using PaparaDotnetBootcampApi.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PaparaDotnetBootcampApi.Entities
{
    /// <summary>
    /// Bu sınıf kart bilgilerini tutar.
    /// </summary>
    public class Card : BaseEntity
    {
        public string CardNumber { get; set; }
        public string NameSurname { get; set; }
        public string ExpiryDate { get; set; }
        public string Cvv { get; set; }

        public int CustomerId { get; set; }
        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
