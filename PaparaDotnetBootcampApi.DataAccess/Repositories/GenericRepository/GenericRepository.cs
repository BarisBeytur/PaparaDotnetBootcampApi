using PaparaDotnetBootcampApi.Core.Entity;
using PaparaDotnetBootcampApi.DataAccess.Context;
using PaparaDotnetBootcampApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly PatikaCohortContext _context;

        public GenericRepository(PatikaCohortContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(int id)
        {
            _context.Set<T>().Remove(GetById(id));
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
