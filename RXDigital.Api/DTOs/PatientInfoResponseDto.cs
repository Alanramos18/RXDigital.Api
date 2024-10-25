namespace RXDigital.Api.DTOs
{
    public class PatientInfoResponseDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string SocialNumber { get; set; }
        public char Gender { get; set; }
        public string Nationality { get; set; }
        //public string Address { get; set; }
        public string? Cellphone { get; set; }
        public string? HomePhone { get; set; }
        public string? SocialWorkName { get; set; }
        public string? SocialPlan { get; set; }
        //public string Email { get; set; }
    }
}
