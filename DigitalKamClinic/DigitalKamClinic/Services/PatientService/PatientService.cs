using DigitalKamClinic.Data;
using DigitalKamClinic.Shared.DTOs;
using DigitalKamClinic.Shared.Entities;
using DigitalKamClinic.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalKamClinic.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly DataContext _context;
        private readonly Guid _tenantId = Guid.Parse("4db9057f-a1c6-47e8-b324-69f0ca12de85");

        public PatientService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Patient>>> GetAllPatientsAsync()
        {
            var response = new ServiceResponse<List<Patient>>();
            try
            {
                var patients = await _context.Patients
                    .Where(p => p.TenantId == _tenantId && p.IsActive)
                    .OrderByDescending(p => p.DateCreated)
                    .ToListAsync();

                response.Data = patients;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading patients: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Patient>> GetPatientByIdAsync(Guid patientId)
        {
            var response = new ServiceResponse<Patient>();
            try
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Id == patientId && p.TenantId == _tenantId);

                if (patient == null)
                {
                    response.Success = false;
                    response.Message = "Patient not found.";
                    return response;
                }

                response.Data = patient;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error loading patient: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Patient>> CreatePatientAsync(PatientCreateDTO patientDto)
        {
            var response = new ServiceResponse<Patient>();
            try
            {
                var patient = new Patient
                {
                    Id = Guid.NewGuid(),
                    TenantId = _tenantId,
                    FirstName = patientDto.FirstName,
                    LastName = patientDto.LastName,
                    DateOfBirth = patientDto.DateOfBirth,
                    Gender = patientDto.Gender,
                    PhoneNumber = patientDto.PhoneNumber,
                    Email = patientDto.Email,
                    Address = patientDto.Address,
                    Allergies = patientDto.Allergies,
                    MedicalConditions = patientDto.MedicalConditions,
                    Medications = patientDto.Medications,
                    EmergencyContactName = patientDto.EmergencyContactName,
                    EmergencyContactPhone = patientDto.EmergencyContactPhone,
                    Notes = patientDto.Notes,
                    IsActive = true,
                    DateCreated = DateTime.UtcNow
                };

                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();

                response.Data = patient;
                response.Success = true;
                response.Message = "Patient created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error creating patient: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<Patient>> UpdatePatientAsync(Guid patientId, PatientCreateDTO patientDto)
        {
            var response = new ServiceResponse<Patient>();
            try
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Id == patientId && p.TenantId == _tenantId);

                if (patient == null)
                {
                    response.Success = false;
                    response.Message = "Patient not found.";
                    return response;
                }

                patient.FirstName = patientDto.FirstName;
                patient.LastName = patientDto.LastName;
                patient.DateOfBirth = patientDto.DateOfBirth;
                patient.Gender = patientDto.Gender;
                patient.PhoneNumber = patientDto.PhoneNumber;
                patient.Email = patientDto.Email;
                patient.Address = patientDto.Address;
                patient.Allergies = patientDto.Allergies;
                patient.MedicalConditions = patientDto.MedicalConditions;
                patient.Medications = patientDto.Medications;
                patient.EmergencyContactName = patientDto.EmergencyContactName;
                patient.EmergencyContactPhone = patientDto.EmergencyContactPhone;
                patient.Notes = patientDto.Notes;
                patient.DateModified = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                response.Data = patient;
                response.Success = true;
                response.Message = "Patient updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error updating patient: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<Patient>>> SearchPatientsAsync(string searchTerm)
        {
            var response = new ServiceResponse<List<Patient>>();
            try
            {
                var patients = await _context.Patients
                    .Where(p => p.TenantId == _tenantId && p.IsActive &&
                           (p.FirstName.Contains(searchTerm) ||
                            p.LastName.Contains(searchTerm) ||
                            p.PhoneNumber.Contains(searchTerm) ||
                            (p.Email != null && p.Email.Contains(searchTerm))))
                    .OrderBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToListAsync();

                response.Data = patients;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error searching patients: {ex.Message}";
            }
            return response;
        }
    }
}
