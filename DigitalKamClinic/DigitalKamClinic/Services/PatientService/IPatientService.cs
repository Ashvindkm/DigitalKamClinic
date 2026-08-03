using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;
using DigitalKamClinic.Shared.Entities;

namespace DigitalKamClinic.Services.PatientService
{
    public interface IPatientService
    {
        Task<ServiceResponse<List<Patient>>> GetAllPatientsAsync();
        Task<ServiceResponse<Patient>> GetPatientByIdAsync(Guid patientId);
        Task<ServiceResponse<Patient>> CreatePatientAsync(PatientCreateDTO patientDto);
        Task<ServiceResponse<Patient>> UpdatePatientAsync(Guid patientId, PatientCreateDTO patientDto);
        Task<ServiceResponse<List<Patient>>> SearchPatientsAsync(string searchTerm);
    }
}
