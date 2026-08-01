using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalKamClinic.Shared.Helpers
{
    public static class Converters
    {
        public static UserDTO GetUserDTOFrom(User userEntity)
        {
            UserDTO userDTO = null;

            try
            {
                if (userEntity != null)
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    userDTO = new UserDTO
                    {
                        TenantId = userEntity.TenantId,
                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        Firsname = userEntity.Firsname,
                        LastName = userEntity.LastName,
                        Whatsapp = userEntity.Whatsapp,
                        PasswordTemp = userEntity.PasswordTemp,
                        PasswordHash = userEntity.PasswordHash,
                        PasswordSalt = userEntity.PasswordSalt,
                        DateCreated = userEntity.DateCreated,
                        DateModified = userEntity.DateModified,
                        LastLoginDate = userEntity.LastLoginDate,
                        Status = userEntity.Status,
                        Tenant = userEntity.Tenant != null ? new TenantDTO
                        {
                            Id = userEntity.Tenant.Id,
                            EnterpriseName = userEntity.Tenant.EnterpriseName,
                            Status = userEntity.Tenant.Status,
                            IsRoot = userEntity.Tenant.IsRoot,
                            DateCreated = userEntity.Tenant.DateCreated,
                            DateModified = userEntity.Tenant.DateModified
                        } : null 
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
                }
            }
            catch (Exception error)
            {
                //Log the error

            }

            return userDTO;
        }
    }
}
