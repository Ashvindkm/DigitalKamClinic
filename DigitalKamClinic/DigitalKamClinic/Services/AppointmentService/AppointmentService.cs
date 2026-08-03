using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalKamClinic.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        private readonly DataContext _context;
        private readonly Guid _tenantId = Guid.Parse("4db9057f-a1c6-47e8-b324-69f0ca12de85");

        public AppointmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Appointment>>> GetAllAppointmentsAsync()
        {
            var response = new ServiceResponse<List<Appointment>>();
            try
            {
                var appointments = await _context.Appointments
                    .OrderByDescending(a => a.DateCreated)
                    .ToListAsync();

                response.Data = appointments;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading appointments: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Appointment>> GetAppointmentByIdAsync(Guid appointmentId)
        {
            var response = new ServiceResponse<Appointment>();
            try
            {
                var appointment = await _context.Appointments.FindAsync(appointmentId);

                if (appointment == null)
                {
                    response.Success = false;
                    response.Message = "Appointment not found.";
                    return response;
                }

                response.Data = appointment;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading appointment: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<Appointment>>> GetAppointmentsByDateAsync(DateTime date)
        {
            var response = new ServiceResponse<List<Appointment>>();
            try
            {
                var dateString = date.ToString("yyyy-MM-dd");
                var appointments = await _context.Appointments
                    .Where(a => a.AppointmentDateSlot == dateString)
                    .OrderBy(a => a.AppointmentTimeSlot)
                    .ToListAsync();

                response.Data = appointments;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading appointments by date: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<Appointment>>> GetTodayAppointmentsAsync()
        {
            var response = new ServiceResponse<List<Appointment>>();
            try
            {
                var today = DateTime.Today.ToString("yyyy-MM-dd");
                var appointments = await _context.Appointments
                    .Where(a => a.AppointmentDateSlot == today)
                    .OrderBy(a => a.AppointmentTimeSlot)
                    .ToListAsync();

                response.Data = appointments;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading today's appointments: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> GetTodayAppointmentCountAsync()
        {
            var response = new ServiceResponse<int>();
            try
            {
                var today = DateTime.Today.ToString("yyyy-MM-dd");
                var count = await _context.Appointments
                    .Where(a => a.AppointmentDateSlot == today && a.Status == 1)
                    .CountAsync();

                response.Data = count;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error counting appointments: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Appointment>> CreateAppointmentAsync(AppointmentCreateDTO appointmentDto)
        {
            var response = new ServiceResponse<Appointment>();
            try
            {
                // Get patient details
                var patient = await _context.Entities.FindAsync(appointmentDto.PatientId);
                var phoneDetail = await _context.EntityDetails
                    .FirstOrDefaultAsync(ed => ed.EntityId == appointmentDto.PatientId && ed.Label == "Phone");
                var emailDetail = await _context.EntityDetails
                    .FirstOrDefaultAsync(ed => ed.EntityId == appointmentDto.PatientId && ed.Label == "Email");

                var appointment = new Appointment
                {
                    Id = Guid.NewGuid(),
                    TenantId = _tenantId,
                    EntityId = appointmentDto.PatientId,
                    LocationId = appointmentDto.LocationId,
                    AppointmentDateSlot = appointmentDto.AppointmentDate.ToString("yyyy-MM-dd"),
                    AppointmentTimeSlot = appointmentDto.AppointmentTime,
                    Status = 1, // Scheduled
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    Firstname = patient?.EntityName?.Split(' ').FirstOrDefault(),
                    Lastname = patient?.EntityName?.Split(' ').Skip(1).FirstOrDefault(),
                    ContactWhatsappNumber = phoneDetail?.Value1,
                    ContactEmailAddress = emailDetail?.Value1
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();

                response.Data = appointment;
                response.Success = true;
                response.Message = "Appointment created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating appointment: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<Appointment>>> FilterAppointmentsAsync(DateTime? date, Guid? locationId, int? status)
        {
            var response = new ServiceResponse<List<Appointment>>();
            try
            {
                var query = _context.Appointments.AsQueryable();

                if (date.HasValue)
                {
                    var dateString = date.Value.ToString("yyyy-MM-dd");
                    query = query.Where(a => a.AppointmentDateSlot == dateString);
                }

                if (locationId.HasValue)
                {
                    query = query.Where(a => a.LocationId == locationId.Value);
                }

                if (status.HasValue)
                {
                    query = query.Where(a => a.Status == status.Value);
                }

                var appointments = await query
                    .OrderBy(a => a.AppointmentDateSlot)
                    .ThenBy(a => a.AppointmentTimeSlot)
                    .ToListAsync();

                response.Data = appointments;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error filtering appointments: {ex.Message}";
            }
            return response;
        }
    }
}
