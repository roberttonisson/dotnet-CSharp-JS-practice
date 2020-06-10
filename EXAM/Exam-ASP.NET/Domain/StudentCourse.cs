using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class StudentCourse
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; } = default!;
        [Display(Name = "Student")]
        public AppUser? AppUser { get; set; }

        public Guid CourseId { get; set; } = default!;
        public Course? Course { get; set; }

        public bool? Accepted { get; set; }
        
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
        public int? Grade { get; set; }
        
        public DateTime? GradedAt { get; set; }
    }
}