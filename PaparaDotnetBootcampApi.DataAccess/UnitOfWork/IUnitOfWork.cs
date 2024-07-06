using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Card> Cards { get; }
        int Complete(); 
    }
}
