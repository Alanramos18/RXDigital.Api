namespace RXDigital.Api.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
    }
}
