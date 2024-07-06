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

    /// <summary>
    /// Bu sınıf, veritabanı işlemlerinde kullanılan genel repository sınıfıdır.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly PatikaCohortContext _context;

        public GenericRepository(PatikaCohortContext context)
        {
            _context = context;
        }

        /// <summary>
        /// veritabanına yeni bir kayıt ekler.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// veritabanından bir kayıt siler.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _context.Set<T>().Remove(GetById(id));
        }

        /// <summary>
        /// veritabanındaki tüm kayıtları getirir.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        /// <summary>
        /// veritabanındaki belirli bir filtreleme işlemine göre kayıtları getirir.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Where(filter).ToList();
        }

        /// <summary>
        /// veritabanındaki belirli bir kaydı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetSingleByFilter(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefault(filter);
        }

        /// <summary>
        /// veritabanındaki belirli bir kaydı günceller.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
