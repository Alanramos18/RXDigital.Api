namespace RXDigital.Api.DTOs
{
    public class GetPrescriptionsResponseDto
    {
        public int PrescriptionId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Diagnostic { get; set; }
        public string? MedicineName { get; set; }
        public int Concentration { get; set; }
    }
}
