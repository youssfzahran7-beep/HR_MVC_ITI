namespace HR_MVC_ITI.DTOs
{
    public class CandidateDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string ResumeFilePath { get; set; } = string.Empty;

    }
}
