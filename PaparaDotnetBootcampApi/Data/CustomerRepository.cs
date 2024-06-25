using PaparaDotnetBootcampApi.Models;

namespace PaparaDotnetBootcampApi.Data
{
    public class CustomerRepository
    {
        private static List<Customer> _customers = new List<Customer>();

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetById(int id)
        {
            return _customers.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Customer Customer)
        {
            Customer.Id = _customers.Count + 1;
            _customers.Add(Customer);
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
                _customers.Remove(Customer);
            }
        }
    }
}
