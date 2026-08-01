using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Services.UserAuthService
{
    public class UserAuthService : IUserAuthService
    {
        private readonly DataContext _context;

        public UserAuthService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<UserDTO>> Signin(string enterprisename, string email, string password)
        {
            ServiceResponse<UserDTO> response = null;
            User? useraccount = null;
            UserDTO userDTO = null;
            bool success = false;
            string message = string.Empty;
            bool proceed = true;
            try
            {
                var tenant = await _context.Tenants
                    .Where(a => EF.Functions.Collate(a.EnterpriseName, "Latin1_General_BIN") == enterprisename)
                    .FirstOrDefaultAsync();

                if (tenant == null)
                {
                    message = "That enterprise name doesn’t match our records. Need help? Reach out to support.";
                    proceed = false;
                }

                if (proceed && tenant.Status == (int)AccountStatus.INACTIVE)
                {
                    message = "The enterprise account is currently inactive. Please contact support for assistance.";
                    proceed = false;
                }

                if (proceed)
                {
                    useraccount = await _context.Users
                        .Include(t => t.Tenant)
                        .FirstOrDefaultAsync(a => a.TenantId == tenant.Id && a.Email == email);

                    if (useraccount == null)
                    {
                        message = "We couldn’t find an account with that enterprise name and username. Please double-check both fields and try again, or reach out to support if you need assistance.";
                        proceed = false;
                    }
                }

                if(proceed && useraccount.Status == (int)AccountStatus.INACTIVE)
                {
                    message = "Your account is currently inactive. Please reach out to your administrator for assistance";
                    proceed = false;
                }

                if (proceed)
                {
                    if (!PasswordHelper.VerifyPasswordHash(password, useraccount.PasswordHash, useraccount.PasswordSalt))
                    {
                        message = "Incorrect password. Try again, or click ‘Forgot Password?";
                        proceed = false;
                    }
                }

                if(proceed)
                {
                    userDTO = Converters.GetUserDTOFrom(useraccount);

                    if(userDTO == null)
                    {
                        message = "An error occurred while processing your request. Please try again later or contact support if the issue persists.";
                    }

                    if(userDTO != null)
                        success = true;
                }

            }
            catch (Exception error)
            {
                message = "We encountered a problem. Please try again, or contact support if it persists.";
            }
            finally
            {
                response = new ServiceResponse<UserDTO>
                {
                    Success = success,
                    Message = message,
                    Data = userDTO
                };

            }

            return response;
        }
    }
}
