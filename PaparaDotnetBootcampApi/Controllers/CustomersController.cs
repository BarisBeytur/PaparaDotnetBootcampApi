﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaDotnetBootcampApi.Data;
using PaparaDotnetBootcampApi.Dtos.Customer;
using PaparaDotnetBootcampApi.Dtos.Result;
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
        public ActionResult<ApiResponse<IEnumerable<Customer>>> GetAll()
        {
            var customers = _repository.GetAll();
            return Ok(ApiResponse<IEnumerable<Customer>>.Success(customers, StatusCodes.Status200OK, "Customers listed successfully"));
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Customer>> GetById(int id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
            {
                return NotFound(ApiResponse<Customer>.Failure("Customer not found", StatusCodes.Status404NotFound));
            }

            return Ok(ApiResponse<Customer>.Success(customer, StatusCodes.Status200OK, "Customer retrieved successfully"));
        }

        [HttpPost]
        public ActionResult<ApiResponse<ResultCustomerDto>> Create([FromBody] CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<ResultCustomerDto>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            Customer customer = new Customer
            {
                Name = createCustomerDto.Name,
                Surname = createCustomerDto.Surname,
                TCKN = createCustomerDto.TCKN
            };

            _repository.Add(customer);

            ResultCustomerDto resultDto = new ResultCustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                TCKN = customer.TCKN
            };

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, ApiResponse<ResultCustomerDto>.Success(resultDto, StatusCodes.Status201Created, "Customer created successfully"));
        }

        [HttpPut("{id}")]
        public ActionResult<ApiResponse<UpdateCustomerDto>> Update(int id, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            if (id != updateCustomerDto.Id)
            {
                return BadRequest(ApiResponse<UpdateCustomerDto>.Failure("ID mismatch", StatusCodes.Status400BadRequest));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<UpdateCustomerDto>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            var existingCustomer = _repository.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound(ApiResponse<UpdateCustomerDto>.Failure("Customer not found", StatusCodes.Status404NotFound));
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
        public ActionResult<ApiResponse<object>> Delete(int id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
            {
                return NotFound(ApiResponse<object>.Failure("Customer not found", StatusCodes.Status404NotFound));
            }

            _repository.Delete(id);

            return NoContent();
        }

        [HttpGet("list")]
        public ActionResult<ApiResponse<IEnumerable<Customer>>> List([FromQuery] string name)
        {
            var customers = _repository.GetAll();

            if (!string.IsNullOrEmpty(name))
            {
                customers = customers.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                if (customers.Count == 0)
                {
                    return NotFound(ApiResponse<IEnumerable<Customer>>.Failure("Customer not found", StatusCodes.Status404NotFound));
                }
            }
            else
            {
                return BadRequest(ApiResponse<IEnumerable<Customer>>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            return Ok(ApiResponse<IEnumerable<Customer>>.Success(customers, StatusCodes.Status200OK, "Customers retrieved successfully"));
        }

    }
}
