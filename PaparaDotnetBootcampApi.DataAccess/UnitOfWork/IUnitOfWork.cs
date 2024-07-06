using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.Entities;
using PaparaDotnetBootcampApi.Entities.Entities;
using PaparaDotnetBootcampApi.DataAccess.Repositories.UserRepository;

namespace PaparaDotnetBootcampApi.DataAccess.UnitOfWork
{
    /// <summary>
    /// bu interface ile UnitOfWork pattern kullanılarak veritabanı işlemleri yapılmaktadır.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Card> Cards { get; }
        IUserRepository Users { get; }
        int Complete(); 
    }
}
