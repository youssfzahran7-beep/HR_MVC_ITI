using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.DTOs
{
    public class RecruitmentDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Requirements { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Open";

        public DateTime CreatedAt { get; set; }
    }
}
