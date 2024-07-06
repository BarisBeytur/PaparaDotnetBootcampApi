using PaparaDotnetBootcampApi.DataAccess.Context;
using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.DataAccess.UnitOfWork;
using PaparaDotnetBootcampApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.DataAccess.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository, IGenericRepository<User>
    {
        private readonly PatikaCohortContext _context;

        public UserRepository(PatikaCohortContext context) : base(context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
