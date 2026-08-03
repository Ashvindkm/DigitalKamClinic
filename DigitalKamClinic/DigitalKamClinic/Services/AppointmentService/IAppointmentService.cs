using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;

namespace DigitalKamClinic.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<List<Appointment>>> GetAllAppointmentsAsync();
        Task<ServiceResponse<Appointment>> GetAppointmentByIdAsync(Guid appointmentId);
        Task<ServiceResponse<List<Appointment>>> GetAppointmentsByDateAsync(DateTime date);
        Task<ServiceResponse<List<Appointment>>> GetTodayAppointmentsAsync();
        Task<ServiceResponse<int>> GetTodayAppointmentCountAsync();
        Task<ServiceResponse<Appointment>> CreateAppointmentAsync(AppointmentCreateDTO appointmentDto);
        Task<ServiceResponse<List<Appointment>>> FilterAppointmentsAsync(DateTime? date, Guid? locationId, int? status);
    }
}
