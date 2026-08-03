using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;

namespace DigitalKamClinic.Services.VisitService
{
    public interface IVisitService
    {
        Task<ServiceResponse<List<Visit>>> GetVisitsByPatientAsync(Guid patientId);
        Task<ServiceResponse<Dictionary<Guid, List<VisitDetail>>>> GetVisitDetailsMapAsync(List<Guid> visitIds);
        Task<ServiceResponse<int>> GetTotalVisitCountAsync();
        Task<ServiceResponse<Visit>> CreateVisitAsync(VisitCreateDTO visitDto);
    }
}
