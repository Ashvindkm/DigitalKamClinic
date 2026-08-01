using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Services.UserAuthService
{
    public interface IUserAuthService
    {
        Task<ServiceResponse<UserDTO>> Signin(string enterprisename, string email, string password);
    }
}
