namespace RXDigital.Api.Entities
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string CommercialName { get; set; }
        public string Description { get; set; }
        public string Presentation { get; set; }
        public string Concentration { get; set; }

        public IEnumerable<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    }
}
