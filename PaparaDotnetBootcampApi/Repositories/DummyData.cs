using PaparaDotnetBootcampApi.Models;
using System;
using System.Collections.Generic;

namespace PaparaDotnetBootcampApi.Repositories
{
    public static class DummyData
    {
        public static List<Customer> Customers { get; set; }
        public static List<Card> Cards { get; set; }

        static DummyData()
        {
            Customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John", Surname = "Doe", TCKN = "12345678901" },
                new Customer { Id = 2, Name = "Jane", Surname = "Smith", TCKN = "23456789012" }
            };

            Cards = new List<Card>
            {
                new Card { Id = 1, CardNumber = "1234567812345678", NameSurname = "John Doe", ExpiryDate = "12/24", Cvv = "123", CustomerId = 1 },
                new Card { Id = 2, CardNumber = "9876543210987654", NameSurname = "Jane Smith", ExpiryDate = "06/25", Cvv = "456", CustomerId = 2 }
            };

            // İlişkiyi kurmak için
            Customers[0].Cards.Add(Cards[0]);
            Customers[1].Cards.Add(Cards[1]);
        }
    }
}
