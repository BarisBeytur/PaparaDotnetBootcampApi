using System.ComponentModel.DataAnnotations;

namespace PaparaDotnetBootcampApi.Dtos.Customer
{
    /// <summary>
    /// bu sınıf, müşteri güncelleme işlemlerinde kullanılacak olan veri transfer nesnesidir.
    /// </summary>
    public class UpdateCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string TCKN { get; set; }
    }
}
