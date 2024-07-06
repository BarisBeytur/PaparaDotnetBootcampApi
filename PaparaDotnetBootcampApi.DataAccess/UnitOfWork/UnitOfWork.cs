using PaparaDotnetBootcampApi.DataAccess.Context;
using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.DataAccess.UnitOfWork
{
    /// <summary>
    /// Bu sınıf, veritabanı işlemlerinin tek bir noktadan yönetilmesini sağlar.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private readonly PatikaCohortContext _context;
        private IGenericRepository<Customer> CustomerRepository;
        private IGenericRepository<Card> CardRepository;

        public UnitOfWork(PatikaCohortContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Customer ve Card tabloları için GenericRepository sınıflarını döndürür.
        /// </summary>
        public IGenericRepository<Customer> Customers => CustomerRepository ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Card> Cards => CardRepository ??= new GenericRepository<Card>(_context);

        /// <summary>
        /// veritabanındaki değişiklikleri kaydeder.
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                    return 1;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex);
                    throw;
                }
            }

        }

        /// <summary>
        /// veritabanı bağlantısını kapatır.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
