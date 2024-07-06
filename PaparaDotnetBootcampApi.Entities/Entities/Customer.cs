using PaparaDotnetBootcampApi.Core.Entity;
using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Entities
{
    /// <summary>
    /// Bu sınıf müşteri bilgilerini tutar
    /// </summary>
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string TCKN { get; set; }


        public virtual ICollection<Card>? Cards { get; set; }
    }
}
