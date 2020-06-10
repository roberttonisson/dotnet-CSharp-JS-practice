using System.Collections;
using System.Collections.Generic;
using Domain;

namespace WebApp.ViewModels
{
    public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; } = default!;
        public string? Search { get; set; }
    }
}