using PaparaDotnetBootcampApi.Models;

namespace PaparaDotnetBootcampApi.Repositories
{
    public class CardRepository
    {

        public IEnumerable<Card> GetAll()
        {
            return DummyData.Cards.ToList();
        }


        public Card GetById(int id)
        {
            return DummyData.Cards.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Card card)
        {
            var customer = DummyData.Customers.FirstOrDefault(p => p.Id == card.CustomerId); 

            card.Id = DummyData.Cards.Count + 1;
            card.NameSurname = $"{customer?.Name} {customer?.Surname}";
            DummyData.Cards.Add(card);
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
                DummyData.Cards.Remove(card);
            }
        }
    }
}
