using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;
using Extensions;

namespace Domain
{
    public class Course
    {
        [Display(Name = "Course")]
        public Guid Id { get; set; }
        [Display(Name = "Name")]
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(128, ErrorMessage = "Maximum length is 128")]
        public string Name { get; set; } = default!;
        
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(128, ErrorMessage = "Maximum length is 128")]
        [Display(Name = "Code")]
        public string Code { get; set; } = default!;
        
                
        [Display(Name = "Semester")]
        public Semester Semester { get; set; } = default!;
        
        [Range(1, 90, ErrorMessage = "Value must be between 1 and 90")]
        public int ECTS { get; set; } = default!;
        
        [Range(1, 9999, ErrorMessage = "Value must be between 1 and 9999")]
        public int Year { get; set; } = default!;

        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(1024, ErrorMessage = "Maximum length is 1024")]
        public string Description { get; set; } = default!;
        
        [Display(Name = "Teacher")]
        public Guid? AppUserId { get; set; }
        [Display(Name = "Student")]
        public AppUser? AppUser { get; set; }
        
        public ICollection<Homework>? Homework { get; set; }
        [Display(Name = "Students")]
        public ICollection<StudentCourse>? StudentCourses { get; set; }
    }
}