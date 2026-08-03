using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;

namespace DigitalKamClinic.Services.LocationService
{
    public interface ILocationService
    {
        Task<ServiceResponse<List<Location>>> GetAllLocationsAsync();
        Task<ServiceResponse<Location>> GetLocationByIdAsync(Guid locationId);
        Task<ServiceResponse<Dictionary<Guid, string>>> GetLocationNamesMapAsync();
        Task<ServiceResponse<int>> GetTotalLocationCountAsync();
        Task<ServiceResponse<Location>> CreateLocationAsync(LocationCreateDTO locationDto);
        Task<ServiceResponse<Location>> UpdateLocationAsync(Guid locationId, LocationCreateDTO locationDto);
        Task<ServiceResponse<Dictionary<Guid, string>>> GetLocationAddressesAsync(List<Guid> locationIds);
    }
}
