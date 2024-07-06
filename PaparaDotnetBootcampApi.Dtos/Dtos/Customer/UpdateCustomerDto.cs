using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Customer
{
    /// <summary>
    /// bu sınıf, müşteri güncelleme işlemlerinde kullanılacak olan veri transfer nesnesidir.
    /// </summary>
    public class UpdateCustomerDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string TCKN { get; set; }
    }
}
