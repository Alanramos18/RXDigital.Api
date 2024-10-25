namespace RXDigital.Api.Entities
{
    public class Status
    {
        public int StatusId { get; set; }
        public string Description { get; set; }

        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
