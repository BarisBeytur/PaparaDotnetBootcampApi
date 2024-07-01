using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaDotnetBootcampApi.Repositories;
using PaparaDotnetBootcampApi.Dtos.Card;
using PaparaDotnetBootcampApi.Dtos.Result;
using PaparaDotnetBootcampApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace PaparaDotnetBootcampApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly CardRepository _cardRepository;
        private readonly CustomerRepository _customerRepository;

        public CardsController()
        {
            _cardRepository = new CardRepository();
            _customerRepository = new CustomerRepository();
        }

        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<Card>>> GetAll()
        {
            var cards = _cardRepository.GetAll();
            return Ok(ApiResponse<IEnumerable<Card>>.Success(cards, StatusCodes.Status200OK, "Cards listed successfully"));
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Card>> GetById(int id)
        {
            var card = _cardRepository.GetById(id);

            if (card == null)
                return NotFound(ApiResponse<Card>.Failure("Card not found", StatusCodes.Status404NotFound));

            return Ok(ApiResponse<Card>.Success(card, StatusCodes.Status200OK, "Card retrieved successfully"));
        }

        [HttpPost]
        public ActionResult<ApiResponse<Card>> Create([FromBody] CreateCardDto createCardDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Card>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));

            Card card = new Card
            {
                CardNumber = createCardDto.CardNumber,
                NameSurname = createCardDto.NameSurname,
                ExpiryDate = createCardDto.ExpiryDate,
                Cvv = createCardDto.Cvv,
                CustomerId = createCardDto.CustomerId
            };

            var customer = _customerRepository.GetById(card.CustomerId);
            if (customer == null)
                return NotFound(ApiResponse<Card>.Failure("Customer not found", StatusCodes.Status404NotFound));

            customer.Cards?.Add(card);

            _cardRepository.Add(card);

            return CreatedAtAction(nameof(GetById), new { id = card.Id }, ApiResponse<Card>.Success(card, StatusCodes.Status201Created, "Card created successfully"));
        }

        [HttpPut("{id}")]
        public ActionResult<ApiResponse<UpdateCardDto>> Update(int id, [FromBody] UpdateCardDto updateCardDto)
        {
            if (id != updateCardDto.Id)
                return BadRequest(ApiResponse<UpdateCardDto>.Failure("ID mismatch", StatusCodes.Status400BadRequest));

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<UpdateCardDto>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));

            var existingCard = _cardRepository.GetById(id);

            if (existingCard == null)
                return NotFound(ApiResponse<UpdateCardDto>.Failure("Card not found", StatusCodes.Status404NotFound));

            Card card = new Card
            {
                Id = id,
                CardNumber = updateCardDto.CardNumber,
                NameSurname = updateCardDto.NameSurname,
                ExpiryDate = updateCardDto.ExpiryDate,
                Cvv = updateCardDto.Cvv,
                CustomerId = updateCardDto.CustomerId
            };

            _cardRepository.Update(card);

            return Ok(ApiResponse<IEnumerable<Card>>.Success(StatusCodes.Status204NoContent, "Card updated successfuly"));
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<object>> Delete(int id)
        {
            var card = _cardRepository.GetById(id);

            if (card == null)
                return NotFound(ApiResponse<object>.Failure("Card not found", StatusCodes.Status404NotFound));

            _cardRepository.Delete(id);

            return Ok(ApiResponse<IEnumerable<Card>>.Success(StatusCodes.Status204NoContent, "Card deleted successfuly"));
        }

        [HttpGet("list")]
        public ActionResult<ApiResponse<IEnumerable<Card>>> List([FromQuery] string nameSurname)
        {
            var cards = _cardRepository.GetAll();

            if (!string.IsNullOrEmpty(nameSurname))
            {
                cards = cards.Where(p => p.NameSurname.Contains(nameSurname, StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.ExpiryDate); // ExpiryDate'e göre sıralama ve Name-Surname'e göre listeleme yapılmaktadır.
            }
            else
            {
                return BadRequest(ApiResponse<IEnumerable<Card>>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            return Ok(ApiResponse<IEnumerable<Card>>.Success(cards, StatusCodes.Status200OK, "Filtered cards retrieved successfully"));
        }


        [HttpPatch("{id}")]
        public ActionResult<ApiResponse<Card>> Patch(int id, [FromBody] JsonPatchDocument<Card> patch)
        {
            var existingCard = _cardRepository.GetById(id);
            if (existingCard == null)
            {
                return NotFound(ApiResponse<Customer>.Failure("Card not found", StatusCodes.Status404NotFound));
            }

            patch.ApplyTo(existingCard);

            return Ok(ApiResponse<Card>.Success(existingCard, StatusCodes.Status200OK, "Card updated successfully"));
        }
    }
}