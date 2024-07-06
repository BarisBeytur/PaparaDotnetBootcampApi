using PaparaDotnetBootcampApi.Core.Response;
using PaparaDotnetBootcampApi.Dtos.Customer;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.Business.Services.Abstract
{
    /// <summary>
    /// Müşteri işlemleri servisi
    /// </summary>
    public interface ICustomerService
    {
        public ApiResponse<IEnumerable<Customer>> GetAllCustomers();
        public ApiResponse<Customer> AddCustomer(CreateCustomerDto customer);
        public ApiResponse<Customer> UpdateCustomer(UpdateCustomerDto customer);
        public ApiResponse<Customer> DeleteCustomer(int id);
        public ApiResponse<Customer> GetCustomerById(int id);
        public ApiResponse<IEnumerable<Customer>> ListCustomersByName(string name);
    }
}
