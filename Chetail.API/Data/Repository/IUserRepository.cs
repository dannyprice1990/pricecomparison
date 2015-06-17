using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chetail.Repository
{
    public interface IUserRepository
    {
        //Task<IdentityResult> RegisterUser(User userModel);

        Task<IdentityUser> FindUser(string userName, string password);

        void Dispose();

    }
}
