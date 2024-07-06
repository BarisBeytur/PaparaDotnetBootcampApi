using PaparaDotnetBootcampApi.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaDotnetBootcampApi.Business.Services.Abstract
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

    }
}
