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
    /// bu interface, tüm entityler için CRUD işlemlerini yapabilmek için oluşturulmuştur.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
        IEnumerable<T> GetByFilter(Expression<Func<T, bool>> filter);

    }
}
