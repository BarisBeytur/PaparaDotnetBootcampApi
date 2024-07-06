using PaparaDotnetBootcampApi.DataAccess.Repositories.GenericRepository;
using PaparaDotnetBootcampApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.DataAccess.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Authenticate(string username, string password);
    }
}
