using PaparaDotnetBootcampApi.Models;

namespace PaparaDotnetBootcampApi.Data
{
    public class CardRepository
    {

        private static List<Card> _cards = new List<Card>();

        public IEnumerable<Card> GetAll()
        {
            return _cards;
        }

        public Card GetById(int id)
        {
            return _cards.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Card card)
        {
            card.Id = _cards.Count + 1;
            _cards.Add(card);
        }

        public void Update(Card card)
        {
            var existingCard = GetById(card.Id);
            if (existingCard != null)
            {
                existingCard.CardNumber = card.CardNumber;
                existingCard.NameSurname = card.NameSurname;
                existingCard.ExpiryDate = card.ExpiryDate;
                existingCard.Cvv = card.Cvv;
                existingCard.CustomerId = card.CustomerId;
            }
        }

        public void Delete(int id)
        {
            var card = GetById(id);
            if (card != null)
            {
                _cards.Remove(card);
            }
        }
    }
}
