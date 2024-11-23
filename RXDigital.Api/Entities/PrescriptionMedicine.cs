namespace RXDigital.Api.Entities
{
    public class PrescriptionMedicine
    {
        public string? PrescriptionCode { get; set; }
        public int MedicineId { get; set; }
        public string Indications { get; set; }

        public Prescription Prescription { get; set; }
        public Medicine Medicine { get; set; }
    }
}
