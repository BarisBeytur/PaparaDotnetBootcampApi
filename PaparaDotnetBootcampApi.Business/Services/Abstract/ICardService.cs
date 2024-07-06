using PaparaDotnetBootcampApi.Core.Response;
using PaparaDotnetBootcampApi.Dtos.Card;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.Business.Services.Abstract
{
    public interface ICardService
    {
        public ApiResponse<IEnumerable<Card>> GetAllCards();
        public ApiResponse<Card> AddCard(CreateCardDto Card);
        public ApiResponse<Card> UpdateCard(UpdateCardDto Card);
        public ApiResponse<Card> DeleteCard(int id);
        public ApiResponse<Card> GetCardById(int id);
        public ApiResponse<IEnumerable<Card>> ListCardsByName(string name);
    }
}
