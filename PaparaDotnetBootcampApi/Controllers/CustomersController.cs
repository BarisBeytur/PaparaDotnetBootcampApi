using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaDotnetBootcampApi.Data;
using PaparaDotnetBootcampApi.Dtos.Customer;
using PaparaDotnetBootcampApi.Models;

namespace PaparaDotnetBootcampApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerRepository _repository;

        public CustomersController()
        {
            _repository = new CustomerRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResultCustomerDto>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ResultCustomerDto> GetById(int id)
        {
            var Customer = _repository.GetById(id);
            if (Customer == null)
            {
                return NotFound(new { status = 404, message = "Customer not found" });
            }
            return Ok(Customer);
        }

        [HttpPost]
        public ActionResult<ResultCustomerDto> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = new Customer
            {
                Name = createCustomerDto.Name,
                Surname = createCustomerDto.Surname,
                TCKN = createCustomerDto.TCKN
            };

            _repository.Add(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = _repository.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound(new { status = 404, message = "Customer not found" });
            }

            Customer customer = new Customer
            {
                Id = updateCustomerDto.Id,
                Name = updateCustomerDto.Name,
                Surname = updateCustomerDto.Surname,
                TCKN = updateCustomerDto.TCKN
            };

            _repository.Update(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Customer = _repository.GetById(id);
            if (Customer == null)
            {
                return NotFound(new { status = 404, message = "Customer not found" });
            }

            _repository.Delete(id);
            return NoContent();
        }



        [HttpGet("list")]
        public ActionResult<IEnumerable<Customer>> List([FromQuery] string name)
        {
            var Customers = _repository.GetAll();
            if (!string.IsNullOrEmpty(name))
            {
                Customers = Customers.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            return Ok(Customers);
        }

    }
}
