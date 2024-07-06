using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.Entities;

namespace PaparaDotnetBootcampApi.DataAccess.UnitOfWork
{
    /// <summary>
    /// bu interface ile UnitOfWork pattern kullanılarak veritabanı işlemleri yapılmaktadır.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Card> Cards { get; }
        int Complete(); 
    }
}
