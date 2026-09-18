using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HR_MVC_ITI.Models.Enitityes
{
    public class Candidate
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string ResumeFilePath { get; set; } = string.Empty;

        public ICollection<ApplicationProcess> Applications { get; set; } = new List<ApplicationProcess>();
    }
}
