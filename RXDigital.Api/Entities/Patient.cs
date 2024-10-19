namespace RXDigital.Api.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string SocialNumber { get; set; }
        public int SocialWorkId { get; set; }

        public SocialWork SocialWork { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
