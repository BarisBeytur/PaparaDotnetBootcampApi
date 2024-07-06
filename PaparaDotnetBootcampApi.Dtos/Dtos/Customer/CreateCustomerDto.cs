using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Customer
{
    /// <summary>
    /// bu sınıf, müşteri oluşturmak için kullanılan veri transfer nesnesidir.
    /// </summary>
    public class CreateCustomerDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string TCKN { get; set; }
    }
}
