namespace RXDigital.Api.Entities
{
    public class SocialWork
    {
        public int SocialWorkId { get; set; }
        public string Name { get; set; }
        public string SocialPlan { get; set; }

        public IEnumerable<Patient> Patients { get; set; }
    }
}
