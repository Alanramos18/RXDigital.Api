namespace RXDigital.Api.Entities
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
