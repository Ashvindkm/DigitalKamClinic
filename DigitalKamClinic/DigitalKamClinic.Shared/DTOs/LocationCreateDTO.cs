namespace DigitalKamClinic.Shared.DTOs
{
    public class LocationCreateDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? OperatingHours { get; set; }
    }
}
