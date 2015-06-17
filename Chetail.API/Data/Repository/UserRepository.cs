using Chetail.API.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Chetail.Repository
{
    public class UserRepository : IUserRepository
    {
        /*  The “UserManager” provides the domain logic for working with user information. 
         *  The “UserManager” knows when to hash a password, 
         *  how and when to validate a user, and how to manage claims.
         */
        private UserManager<IdentityUser> _userManager;

        AppDBContext _ctx;
        public UserRepository(AppDBContext ctx)
        {
            _ctx = ctx;
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        // register a user
        //public async Task<IdentityResult> RegisterUser(User userModel)
        //{
        //    IdentityUser user = new IdentityUser
        //    {
        //        UserName = userModel.UserName
        //    };
 
        //    var result = await _userManager.CreateAsync(user, userModel.Password);
 
        //    return result;
        //}
 
        // Finds a user by userName & password
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);
 
            return user;
        }
 
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
 
        }
    }
}