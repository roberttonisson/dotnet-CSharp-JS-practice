using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Homework
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; } = default!;
        public Course? Course { get; set; }
        
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(1024, ErrorMessage = "Maximum length is 1024")]
        public string Title { get; set; } = default!;
        
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(1024, ErrorMessage = "Maximum length is 1024")]
        public string Description { get; set; } = default!;
        
        public DateTime? Deadline { get; set; }

        public ICollection<StudentHomework>? StudentHomework { get; set; }
    }
}