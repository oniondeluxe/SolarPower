using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionDlx.SolPwr.Services
{
    internal class UserAuthService : IUserAuthService
    {
        readonly UserManager<IdentityUser> _userMgr;

        public UserAuthService(UserManager<IdentityUser> userMgr)
        {
            _userMgr = userMgr;
        }
    }
}
