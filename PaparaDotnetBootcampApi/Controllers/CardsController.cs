using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaDotnetBootcampApi.Data;
using PaparaDotnetBootcampApi.Dtos.Card;
using PaparaDotnetBootcampApi.Models;

namespace PaparaDotnetBootcampApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {

        private readonly CardRepository _repository;
        private readonly CustomerRepository _customerRepository;

        public CardsController()
        {
            _repository = new CardRepository();
            _customerRepository = new CustomerRepository();
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResultCardDto>> GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ResultCardDto> GetById(int id)
        {
            var Card = _repository.GetById(id);

            if (Card == null)
                throw new KeyNotFoundException("Card not found");

            return Ok(Card);
        }

        [HttpPost]
        public ActionResult<ResultCardDto> Create([FromBody] CreateCardDto createCardDto)
        {
            if (!ModelState.IsValid)
                throw new ArgumentException("Invalid request parameters");

            Card card = new Card
            {
                CardNumber = createCardDto.CardNumber,
                NameSurname = createCardDto.NameSurname,
                ExpiryDate = createCardDto.ExpiryDate,
                Cvv = createCardDto.Cvv,
                CustomerId = createCardDto.CustomerId
            };

            var customer = _customerRepository.GetById(card.CustomerId);
            customer.Cards?.Add(card);

            _repository.Add(card);
            return CreatedAtAction(nameof(GetById), new { id = card.Id }, card);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateCardDto updateCardDto)
        {
            if (id != updateCardDto.Id)
                throw new ArgumentException("ID mismatch");

            if (!ModelState.IsValid)
                throw new ArgumentException("Invalid request parameters");

            var existingCard = _repository.GetById(id);

            if (existingCard == null)
                throw new KeyNotFoundException("Card not found");

            Card card = new Card
            {
                Id = id,
                CardNumber = updateCardDto.CardNumber,
                NameSurname = updateCardDto.NameSurname,
                ExpiryDate = updateCardDto.ExpiryDate,
                Cvv = updateCardDto.Cvv,
                CustomerId = updateCardDto.CustomerId
            };

            _repository.Update(card);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Card = _repository.GetById(id);

            if (Card == null)
                throw new KeyNotFoundException("Card not found");

            _repository.Delete(id);
            return NoContent();
        }



        [HttpGet("list")]
        public ActionResult<IEnumerable<Card>> List([FromQuery] string nameSurname)
        {
            var Cards = _repository.GetAll();
            if (!string.IsNullOrEmpty(nameSurname))
            {
                Cards = Cards.Where(p => p.NameSurname.Contains(nameSurname, StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                throw new ArgumentException("Invalid request parameters");
            }

            return Ok(Cards);
        }
    }
}
