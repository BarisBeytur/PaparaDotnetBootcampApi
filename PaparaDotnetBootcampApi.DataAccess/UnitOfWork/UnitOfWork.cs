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
    public class UnitOfWork : IUnitOfWork
    {

        private readonly PatikaCohortContext _context;
        private IGenericRepository<Customer> CustomerRepository;
        private IGenericRepository<Card> CardRepository;

        public UnitOfWork(PatikaCohortContext context)
        {
            _context = context;
        }

        public IGenericRepository<Customer> Customers => CustomerRepository ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Card> Cards => CardRepository ??= new GenericRepository<Card>(_context);

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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
