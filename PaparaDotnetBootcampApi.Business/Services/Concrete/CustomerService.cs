using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.Core.Response;
using PaparaDotnetBootcampApi.DataAccess.UnitOfWork;
using PaparaDotnetBootcampApi.Dtos.Customer;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.Business.Services.Concrete
{
    /// <summary>
    /// Müşteri işlemleri servisi
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Tüm müşterileri listeler.
        /// </summary>
        /// <returns></returns>
        public ApiResponse<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = _unitOfWork.Customers.GetAll();
            return ApiResponse<IEnumerable<Customer>>.Success(customers, StatusCodes.Status200OK, "Customers listed successfully");
        }


        /// <summary>
        /// Müşteri oluşturur.
        /// </summary>
        /// <param name="createCustomerDto"></param>
        /// <returns></returns>
        public ApiResponse<Customer> AddCustomer(CreateCustomerDto createCustomerDto)
        {
            Customer customer = new Customer
            {
                Name = createCustomerDto.Name,
                Surname = createCustomerDto.Surname,
                TCKN = createCustomerDto.TCKN
            };

            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();

            return ApiResponse<Customer>.Success(customer, StatusCodes.Status201Created, "Customer created successfully");
        }


        /// <summary>
        /// Müşteri bilgilerini günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCustomerDto"></param>
        /// <returns></returns>
        public ApiResponse<Customer> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {

            var getCustomerResponse = GetCustomerById(updateCustomerDto.Id);

            if (getCustomerResponse.IsSuccessFul)
            {
                getCustomerResponse.Data.Name = updateCustomerDto.Name;
                getCustomerResponse.Data.Surname = updateCustomerDto.Surname;
                getCustomerResponse.Data.TCKN = updateCustomerDto.TCKN;

                _unitOfWork.Customers.Update(getCustomerResponse.Data);
                _unitOfWork.Complete();

                return ApiResponse<Customer>.Success(getCustomerResponse.Data, StatusCodes.Status200OK, "Customer updated successfully");
            }

            return ApiResponse<Customer>.Failure(getCustomerResponse.Message, getCustomerResponse.StatusCode);
        }


        /// <summary>
        /// Müşteriyi siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResponse<Customer> DeleteCustomer(int id)
        {

            var customer = _unitOfWork.Customers.GetById(id);

            if (customer == null)
            {
                return ApiResponse<Customer>.Failure("Customer not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.Customers.Delete(customer.Id);
            _unitOfWork.Complete();

            return ApiResponse<Customer>.Success(StatusCodes.Status204NoContent, "Customer deleted successfully");
        }


        /// <summary>
        /// Id değerine göre müşteri bilgilerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResponse<Customer> GetCustomerById(int id)
        {
            var customer = _unitOfWork.Customers.GetById(id);

            if (customer == null)
            {
                return ApiResponse<Customer>.Failure("Customer not found", StatusCodes.Status404NotFound);
            }

            return ApiResponse<Customer>.Success(customer, StatusCodes.Status200OK, "Customer retrieved successfully");
        }


        /// <summary>
        /// Müşterileri adına göre listeler ve TCKN'ye göre sıralar.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<Customer>> ListCustomersByName(string name)
        {
            List<Customer> customers;

            if (!string.IsNullOrEmpty(name))
            {
                customers = _unitOfWork.Customers.GetByFilter(p => EF.Functions.Like(p.Name, $"%{name}%")).OrderBy(x => x.TCKN).ToList();

                if (!customers.Any())
                    return ApiResponse<IEnumerable<Customer>>.Failure("Customer not found", StatusCodes.Status404NotFound);
            }
            else
                return ApiResponse<IEnumerable<Customer>>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest);


            return ApiResponse<IEnumerable<Customer>>.Success(customers, StatusCodes.Status200OK, "Customers retrieved successfully");
        }


    }
}


