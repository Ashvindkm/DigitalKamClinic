using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalKamClinic.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly DataContext _context;
        private readonly Guid _tenantId = Guid.Parse("4db9057f-a1c6-47e8-b324-69f0ca12de85");

        public LocationService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Location>>> GetAllLocationsAsync()
        {
            var response = new ServiceResponse<List<Location>>();
            try
            {
                var locations = await _context.Locations
                    .OrderBy(l => l.Name)
                    .ToListAsync();

                response.Data = locations;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading locations: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Location>> GetLocationByIdAsync(Guid locationId)
        {
            var response = new ServiceResponse<Location>();
            try
            {
                var location = await _context.Locations.FindAsync(locationId);

                if (location == null)
                {
                    response.Success = false;
                    response.Message = "Location not found.";
                    return response;
                }

                response.Data = location;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading location: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Dictionary<Guid, string>>> GetLocationNamesMapAsync()
        {
            var response = new ServiceResponse<Dictionary<Guid, string>>();
            try
            {
                var locations = await _context.Locations.ToListAsync();
                var locationNames = new Dictionary<Guid, string>();

                foreach (var location in locations)
                {
                    if (!string.IsNullOrEmpty(location.Name))
                    {
                        locationNames[location.Id] = location.Name;
                    }
                }

                response.Data = locationNames;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading location names: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> GetTotalLocationCountAsync()
        {
            var response = new ServiceResponse<int>();
            try
            {
                var count = await _context.Locations.CountAsync();
                response.Data = count;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error counting locations: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Location>> CreateLocationAsync(LocationCreateDTO locationDto)
        {
            var response = new ServiceResponse<Location>();
            try
            {
                // Create address
                var address = new Address
                {
                    Id = Guid.NewGuid(),
                    TenantId = _tenantId,
                    StreetAddress = locationDto.Address,
                    DateCreated = DateTime.UtcNow
                };
                _context.Address.Add(address);

                // Create location
                var location = new Location
                {
                    Id = Guid.NewGuid(),
                    TenantId = _tenantId,
                    Name = locationDto.Name,
                    Description = locationDto.Description,
                    AddressId = address.Id,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                };

                _context.Locations.Add(location);
                await _context.SaveChangesAsync();

                response.Data = location;
                response.Success = true;
                response.Message = "Location created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating location: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Location>> UpdateLocationAsync(Guid locationId, LocationCreateDTO locationDto)
        {
            var response = new ServiceResponse<Location>();
            try
            {
                var location = await _context.Locations.FindAsync(locationId);

                if (location == null)
                {
                    response.Success = false;
                    response.Message = "Location not found.";
                    return response;
                }

                location.Name = locationDto.Name;
                location.Description = locationDto.Description;
                location.DateModified = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Data = location;
                response.Success = true;
                response.Message = "Location updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating location: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Dictionary<Guid, string>>> GetLocationAddressesAsync(List<Guid> locationIds)
        {
            var response = new ServiceResponse<Dictionary<Guid, string>>();
            try
            {
                var addresses = new Dictionary<Guid, string>();
                var locations = await _context.Locations
                    .Where(l => locationIds.Contains(l.Id))
                    .ToListAsync();

                foreach (var location in locations)
                {
                    if (location.AddressId.HasValue)
                    {
                        var address = await _context.Address.FindAsync(location.AddressId.Value);
                        if (address != null)
                        {
                            addresses[location.Id] = $"{address.StreetAddress}, {address.City}";
                        }
                    }
                }

                response.Data = addresses;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading location addresses: {ex.Message}";
            }
            return response;
        }
    }
}
