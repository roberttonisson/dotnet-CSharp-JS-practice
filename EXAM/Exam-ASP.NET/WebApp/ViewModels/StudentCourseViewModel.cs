using System;
using System.Collections.Generic;
using Domain;

namespace WebApp.ViewModels
{
    public class StudentCourseViewModel
    {
        public IEnumerable<StudentCourse> StudentCourses { get; set; } = default!;
        public string? Search { get; set; }
        public Guid CourseId { get; set; } = default!;
        public StudentCourse? StudentCourse = new StudentCourse();
    }
}