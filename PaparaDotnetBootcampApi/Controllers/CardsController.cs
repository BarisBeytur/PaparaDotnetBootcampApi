using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.Core.Response;
using PaparaDotnetBootcampApi.Dtos.Card;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.Controllers
{
    /// <summary>
    /// Bu controller sınıfı, kartlar ile ilgili işlemleri gerçekleştirmek için kullanılır.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }


        /// <summary>
        /// Tüm kartları listeler.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<Card>>> GetAll()
        {
            var response = _cardService.GetAllCards();

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// ID değeri verilen kartı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Card>> GetById(int id)
        {
            var response = _cardService.GetCardById(id);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Kart oluşturur.
        /// </summary>
        /// <param name="createCardDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResponse<Card>> Create([FromBody] CreateCardDto createCardDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<Card>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            var response = _cardService.AddCard(createCardDto);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Data.Id }, response);
        }


        /// <summary>
        /// Kartı günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCardDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<ApiResponse<Card>> Update(int id, [FromBody] UpdateCardDto updateCardDto)
        {
            if (id != updateCardDto.Id)
            {
                return BadRequest(ApiResponse<Card>.Failure("ID mismatch", StatusCodes.Status400BadRequest));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<Card>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest));
            }

            var response = _cardService.UpdateCard(updateCardDto);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Kart siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<object>> Delete(int id)
        {
            var response = _cardService.DeleteCard(id);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Kart sahibinin adına göre listeleme ve kart numarasına göre sıralama yapar.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public ActionResult<ApiResponse<IEnumerable<Card>>> List([FromQuery] string name)
        {
            var response = _cardService.ListCardsByName(name);

            if (!response.IsSuccessFul)
            {
                return StatusCode(response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Kartın ilgili alanlarını günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public ActionResult<ApiResponse<Card>> Patch(int id, [FromBody] JsonPatchDocument<Card> patch)
        {
            var existingCard = _cardService.GetCardById(id);

            if (existingCard == null)
            {
                return NotFound(ApiResponse<Card>.Failure("Card not found", StatusCodes.Status404NotFound));
            }

            patch.ApplyTo(existingCard.Data);

            UpdateCardDto dto = new UpdateCardDto
            {
                Id = existingCard.Data.Id,
                CardNumber = existingCard.Data.CardNumber,
                NameSurname = existingCard.Data.NameSurname,
                ExpiryDate = existingCard.Data.ExpiryDate,
                Cvv = existingCard.Data.Cvv,
                CustomerId = existingCard.Data.CustomerId
            };

            _cardService.UpdateCard(dto);

            return Ok(ApiResponse<Card>.Success(existingCard.Data, StatusCodes.Status200OK, "Card updated successfully"));
        }
    }
}