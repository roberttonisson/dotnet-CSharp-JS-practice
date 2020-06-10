using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class StudentHomework
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; } = default!;
        [Display(Name = "Student")]
        public AppUser? AppUser { get; set; }

        public Guid HomeworkId { get; set; } = default!;
        public Homework? Homework { get; set; }
        
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
        public int? Grade { get; set; } = default!;
        
        public DateTime? GradedAt { get; set; }
        
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(2048, ErrorMessage = "Maximum length is 2048")]
        [Display(Name = "Student's answer")]
        public string? StudentAnswer { get; set; }
    }
}