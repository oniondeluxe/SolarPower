using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnionDlx.SolPwr.Configuration;
using OnionDlx.SolPwr.Dto;
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

        public async Task<UserAuthResponse> RegisterUserAsync(UserAccountRegistration registration)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(UserAccountRegistration));
            }

            if (registration.Password != registration.ConfirmPassword)
            {
                var response = new UserAuthResponse
                {
                    Success = false,
                    Message = Messages.ERR_PWD_MISMATCH
                };
            }

            var userRecord = new IdentityUser
            {
                UserName = registration.Email,
                Email = registration.Email,
            };

            var result = await _userMgr.CreateAsync(userRecord, registration.Password);
            if (!result.Succeeded)
            {
                var response = new UserAuthResponse
                {
                    Success = false,
                    Message = result.Errors.FirstOrDefault()?.Description,
                    ErrorInfo = from err in result.Errors select (err.Code, err.Description)
                };
                return response;
            }

            return new UserAuthResponse
            {
                Success = true,
                Message = Messages.MSG_ACCT_CREATED
            };
        }


        public UserAuthService(UserManager<IdentityUser> userMgr)
        {
            _userMgr = userMgr;
        }
    }
}
