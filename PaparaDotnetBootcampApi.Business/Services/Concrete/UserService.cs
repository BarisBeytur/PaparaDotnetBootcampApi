using PaparaDotnetBootcampApi.Business.Services.Abstract;
using PaparaDotnetBootcampApi.DataAccess.Repositories.UserRepository;
using PaparaDotnetBootcampApi.DataAccess.UnitOfWork;
using PaparaDotnetBootcampApi.Entities.Entities;

namespace PaparaDotnetBootcampApi.Business.Services.Concrete
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Authenticate(string username, string password)
        {
            return _unitOfWork.Users.Authenticate(username, password);
        }
    }
}
