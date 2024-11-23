namespace RXDigital.Api.Entities
{
    public class Prescription
    {
        public string? PrescriptionCode { get; set; }
        public string? Diagnostic { get; set; }
        public string? Indications { get; set; }
        public string? MotivoRechazo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Expiration { get; set; }

        public int RegistrationId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public int? PharmaceuticalRegistrationId { get; set; }
        public Pharmaceutical? Pharmaceutical { get; set; }

        public IEnumerable<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    }
}
