using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.Core.Response;
using PaparaDotnetBootcampApi.Dtos.Customer;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.Controllers
{
    /// <summary>
    /// Bu controller sınıfı, müşteri işlemleri için kullanılmaktadır.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Tüm müşterileri listeler.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<Customer>>> GetAll()
        {
            var response = _customerService.GetAllCustomers();

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Id değerine göre müşteri bilgilerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Customer>> GetById(int id)
        {
            var response = _customerService.GetCustomerById(id);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Müşteri oluşturur.
        /// </summary>
        /// <param name="createCustomerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResponse<Customer>> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<Customer>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            var response = _customerService.AddCustomer(createCustomerDto);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }


        /// <summary>
        /// Müşteri bilgilerini günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCustomerDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<ApiResponse<Customer>> Update(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
            {
                return BadRequest(ApiResponse<Customer>.Failure("ID mismatch", StatusCodes.Status400BadRequest));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<Customer>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            var response = _customerService.UpdateCustomer(updateCustomerDto);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }

        
        /// <summary>
        /// Müşteriyi siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<object>> Delete(int id)
        {
            var response = _customerService.DeleteCustomer(id);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Müşterileri adına göre listeler ve TCKN'ye göre sıralar.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public ActionResult<ApiResponse<IEnumerable<Customer>>> List([FromQuery] string name)
        {
            var response = _customerService.ListCustomersByName(name);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Müşterinin ilgili alanlarını günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public ActionResult<ApiResponse<Customer>> Patch(int id, [FromBody] JsonPatchDocument<Customer> patch)
        {
            var existingCustomer = _customerService.GetCustomerById(id);

            if (existingCustomer == null)
            {
                return NotFound(ApiResponse<Customer>.Failure("Customer not found", StatusCodes.Status404NotFound));
            }

            patch.ApplyTo(existingCustomer.Data);

            UpdateCustomerDto dto = new UpdateCustomerDto
            {
                Id = existingCustomer.Data.Id,
                Name = existingCustomer.Data.Name,
                Surname = existingCustomer.Data.Surname,
                TCKN = existingCustomer.Data.TCKN       
            };

            _customerService.UpdateCustomer(dto);

            return Ok(ApiResponse<Customer>.Success(existingCustomer.Data, StatusCodes.Status200OK, "Customer updated successfully"));
        }
    }

}
