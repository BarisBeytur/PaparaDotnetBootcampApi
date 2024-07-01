using PaparaDotnetBootcampApi.Models;

namespace PaparaDotnetBootcampApi.Repositories
{
    public class CustomerRepository
    {

        public List<Customer> GetAll()
        {
            return DummyData.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return DummyData.Customers.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Customer Customer)
        {
            Customer.Id = DummyData.Customers.Count + 1;
            DummyData.Customers.Add(Customer);
        }

        public void Update(Customer Customer)
        {
            var existingCustomer = GetById(Customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.TCKN = Customer.TCKN;
                existingCustomer.Name = Customer.Name;
                existingCustomer.Surname = Customer.Surname;
                existingCustomer.Id = Customer.Id;
            }
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            var cards = DummyData.Cards.Where(p => p.CustomerId == id).ToList();
            if (customer != null)
            {
                DummyData.Customers.Remove(customer);
                foreach(var item in cards)
                {
                    DummyData.Cards.Remove(item);

                }
            }
        }
    }
}
