using PaparaDotnetBootcampApi.Models;
using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Customer
{
    public class ResultCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TCKN { get; set; }

        public ICollection<PaparaDotnetBootcampApi.Models.Card>? Cards { get; set; }
    }
}
