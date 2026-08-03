using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalKamClinic.Services.VisitService
{
    public class VisitService : IVisitService
    {
        private readonly DataContext _context;
        private readonly Guid _tenantId = Guid.Parse("4db9057f-a1c6-47e8-b324-69f0ca12de85");

        public VisitService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Visit>>> GetVisitsByPatientAsync(Guid patientId)
        {
            var response = new ServiceResponse<List<Visit>>();
            try
            {
                // Get appointments for this patient
                var appointments = await _context.Appointments
                    .Where(a => a.EntityId == patientId)
                    .Select(a => a.Id)
                    .ToListAsync();

                if (!appointments.Any())
                {
                    response.Data = new List<Visit>();
                    response.Success = true;
                    return response;
                }

                // Get visits for these appointments
                var visits = await _context.Visits
                    .Where(v => appointments.Contains(v.AppointmentId.Value))
                    .ToListAsync();

                response.Data = visits;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading visits: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Dictionary<Guid, List<VisitDetail>>>> GetVisitDetailsMapAsync(List<Guid> visitIds)
        {
            var response = new ServiceResponse<Dictionary<Guid, List<VisitDetail>>>();
            try
            {
                var detailsMap = new Dictionary<Guid, List<VisitDetail>>();

                if (!visitIds.Any())
                {
                    response.Data = detailsMap;
                    response.Success = true;
                    return response;
                }

                var allVisitDetails = await _context.VisitDetails
                    .Where(vd => visitIds.Contains(vd.VisitId.Value))
                    .ToListAsync();

                foreach (var visitId in visitIds)
                {
                    detailsMap[visitId] = allVisitDetails
                        .Where(vd => vd.VisitId == visitId)
                        .ToList();
                }

                response.Data = detailsMap;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading visit details: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> GetTotalVisitCountAsync()
        {
            var response = new ServiceResponse<int>();
            try
            {
                var count = await _context.Visits.CountAsync();
                response.Data = count;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error counting visits: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Visit>> CreateVisitAsync(VisitCreateDTO visitDto)
        {
            var response = new ServiceResponse<Visit>();
            try
            {
                Guid? effectiveAppointmentId = visitDto.AppointmentId;

                // If no appointment linked, create a walk-in appointment record
                if (effectiveAppointmentId == null || effectiveAppointmentId == Guid.Empty)
                {
                    var newAppointment = new Appointment
                    {
                        Id = Guid.NewGuid(),
                        TenantId = _tenantId,
                        EntityId = visitDto.PatientId,
                        LocationId = visitDto.LocationId,
                        AppointmentDateSlot = visitDto.VisitDate.ToString("yyyy-MM-dd"),
                        AppointmentTimeSlot = "Walk-in",
                        Status = 2, // Completed
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow
                    };
                    _context.Appointments.Add(newAppointment);
                    effectiveAppointmentId = newAppointment.Id;
                }
                else
                {
                    // Update appointment status to completed
                    var appointment = await _context.Appointments.FindAsync(effectiveAppointmentId);
                    if (appointment != null)
                    {
                        appointment.Status = 2;
                        appointment.DateModified = DateTime.UtcNow;
                    }
                }

                // Create visit record
                var visit = new Visit
                {
                    Id = Guid.NewGuid(),
                    TenantId = _tenantId,
                    AppointmentId = effectiveAppointmentId,
                    LocationId = visitDto.LocationId,
                    DateCreated = visitDto.VisitDate,
                    DateModified = DateTime.UtcNow,
                    Status = 1 // Completed
                };

                _context.Visits.Add(visit);

                // Create visit details
                var details = new List<VisitDetail>();

                if (!string.IsNullOrWhiteSpace(visitDto.ChiefComplaint))
                    AddVisitDetail(details, visit.Id, "ChiefComplaint", visitDto.ChiefComplaint);
                AddVisitDetail(details, visit.Id, "Treatment", visitDto.TreatmentPerformed);
                if (!string.IsNullOrWhiteSpace(visitDto.Diagnosis))
                    AddVisitDetail(details, visit.Id, "Diagnosis", visitDto.Diagnosis);
                if (!string.IsNullOrWhiteSpace(visitDto.ClinicalNotes))
                    AddVisitDetail(details, visit.Id, "Notes", visitDto.ClinicalNotes);
                if (!string.IsNullOrWhiteSpace(visitDto.Prescription))
                    AddVisitDetail(details, visit.Id, "Prescription", visitDto.Prescription);
                if (!string.IsNullOrWhiteSpace(visitDto.NextVisitAdvice))
                    AddVisitDetail(details, visit.Id, "NextVisitAdvice", visitDto.NextVisitAdvice);
                if (visitDto.Cost.HasValue)
                    AddVisitDetail(details, visit.Id, "Cost", visitDto.Cost.Value.ToString("F2"));
                if (!string.IsNullOrWhiteSpace(visitDto.PaymentStatus))
                    AddVisitDetail(details, visit.Id, "PaymentStatus", visitDto.PaymentStatus);
                if (!string.IsNullOrWhiteSpace(visitDto.ToothNumbers))
                    AddVisitDetail(details, visit.Id, "ToothNumbers", visitDto.ToothNumbers);

                _context.VisitDetails.AddRange(details);
                await _context.SaveChangesAsync();

                response.Data = visit;
                response.Success = true;
                response.Message = "Visit recorded successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating visit: {ex.Message}";
            }
            return response;
        }

        private void AddVisitDetail(List<VisitDetail> details, Guid visitId, string label, string value)
        {
            details.Add(new VisitDetail
            {
                Id = Guid.NewGuid(),
                TenantId = _tenantId,
                VisitId = visitId,
                Label = label,
                Value1 = value,
                DateCreated = DateTime.UtcNow
            });
        }
    }
}
