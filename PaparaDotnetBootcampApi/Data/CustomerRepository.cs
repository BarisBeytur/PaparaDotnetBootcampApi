using PaparaDotnetBootcampApi.Models;

namespace PaparaDotnetBootcampApi.Data
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
            var Customer = GetById(id);
            if (Customer != null)
            {
                DummyData.Customers.Remove(Customer);
            }
        }
    }
}
