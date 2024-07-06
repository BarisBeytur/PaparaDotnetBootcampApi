using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Customer
{
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
