using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Customer
{
    /// <summary>
    /// bu sınıf, müşteri oluşturmak için kullanılan veri transfer nesnesidir.
    /// </summary>
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TCKN { get; set; }
    }
}
