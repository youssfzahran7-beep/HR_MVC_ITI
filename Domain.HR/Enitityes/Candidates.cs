using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.HR.Enitityes
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
        }
    }

