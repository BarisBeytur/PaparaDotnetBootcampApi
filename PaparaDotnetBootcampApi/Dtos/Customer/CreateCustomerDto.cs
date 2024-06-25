using PaparaDotnetBootcampApi.Models;
using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Customer
{
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
