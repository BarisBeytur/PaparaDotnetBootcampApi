using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.Core.Response;
using PaparaDotnetBootcampApi.DataAccess.UnitOfWork;
using PaparaDotnetBootcampApi.Dtos.Card;
using PaparaDotnetBootcampApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.Business.Services.Concrete
{
    /// <summary>
    /// Kart işlemleri servisi
    /// </summary>
    public class CardService : ICardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Tüm kartları listeler.
        /// </summary>
        /// <returns></returns>
        public ApiResponse<IEnumerable<Card>> GetAllCards()
        {
            var cards = _unitOfWork.Cards.GetAll();
            return ApiResponse<IEnumerable<Card>>.Success(cards, StatusCodes.Status200OK, "Cards listed successfully");
        }


        /// <summary>
        /// Kart oluşturur.
        /// </summary>
        /// <param name="createCardDto"></param>
        /// <returns></returns>
        public ApiResponse<Card> AddCard(CreateCardDto createCardDto)
        {
            var customer = _unitOfWork.Customers.GetById(createCardDto.CustomerId);

            if (customer == null)
            {
                return ApiResponse<Card>.Failure("Customer not found", StatusCodes.Status404NotFound);
            }

            Card card = new Card
            {
                CardNumber = createCardDto.CardNumber,
                NameSurname = createCardDto.NameSurname,
                ExpiryDate = createCardDto.ExpiryDate,
                Cvv = createCardDto.Cvv,
                CustomerId = createCardDto.CustomerId
            };

            _unitOfWork.Cards.Add(card);
            _unitOfWork.Complete();

            return ApiResponse<Card>.Success(card, StatusCodes.Status201Created, "Card created successfully");
        }


        /// <summary>
        /// Kartı günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCardDto"></param>
        /// <returns></returns>
        public ApiResponse<Card> UpdateCard(UpdateCardDto updateCardDto)
        {

            var getCardResponse = GetCardById(updateCardDto.Id);

            if (getCardResponse.IsSuccessFul)
            {
                getCardResponse.Data.CardNumber = updateCardDto.CardNumber;
                getCardResponse.Data.NameSurname = updateCardDto.NameSurname;
                getCardResponse.Data.ExpiryDate = updateCardDto.ExpiryDate;
                getCardResponse.Data.Cvv = updateCardDto.Cvv;
                getCardResponse.Data.CustomerId = updateCardDto.CustomerId;

                _unitOfWork.Cards.Update(getCardResponse.Data);
                _unitOfWork.Complete();

                return ApiResponse<Card>.Success(getCardResponse.Data, StatusCodes.Status200OK, "Card updated successfully");
            }

            return ApiResponse<Card>.Failure(getCardResponse.Message, getCardResponse.StatusCode);
        }


        /// <summary>
        /// Kart siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResponse<Card> DeleteCard(int id)
        {

            var card = _unitOfWork.Cards.GetById(id);

            if (card == null)
            {
                return ApiResponse<Card>.Failure("Card not found", StatusCodes.Status404NotFound);
            }

            _unitOfWork.Cards.Delete(card.Id);
            _unitOfWork.Complete();

            return ApiResponse<Card>.Success(StatusCodes.Status204NoContent, "Card deleted successfully");
        }


        /// <summary>
        /// ID değeri verilen kartı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResponse<Card> GetCardById(int id)
        {
            var card = _unitOfWork.Cards.GetById(id);

            if (card == null)
            {
                return ApiResponse<Card>.Failure("Card not found", StatusCodes.Status404NotFound);
            }

            return ApiResponse<Card>.Success(card, StatusCodes.Status200OK, "Card retrieved successfully");
        }

        /// <summary>
        /// Kart sahibinin ad soyadına göre listeleme ve kart numarasına göre sıralama yapar.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ApiResponse<IEnumerable<Card>> ListCardsByName(string nameSurname)
        {
            List<Card> cards;

            if (!string.IsNullOrEmpty(nameSurname))
            {
                cards = _unitOfWork.Cards.GetByFilter(p => EF.Functions.Like(p.NameSurname, $"%{nameSurname}%")).OrderBy(x => x.CardNumber).ToList();

                if (!cards.Any())
                    return ApiResponse<IEnumerable<Card>>.Failure("Card not found", StatusCodes.Status404NotFound);
            }
            else
                return ApiResponse<IEnumerable<Card>>.Failure("Invalid request parameters", StatusCodes.Status400BadRequest);


            return ApiResponse<IEnumerable<Card>>.Success(cards, StatusCodes.Status200OK, "Cards retrieved successfully");
        }
    }
}
