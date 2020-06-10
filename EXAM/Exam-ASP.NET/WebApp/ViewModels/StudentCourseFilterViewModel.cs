using System.Collections.Generic;
using DAL.App.EF.Helpers;
using Domain;
using Extensions;

namespace WebApp.ViewModels
{
    public class StudentCourseFilterViewModel
    {
        public IEnumerable<StudentCourse> StudentCourses { get; set; } = default!;
        public Semester? Semester { get; set; }
        public int Year { get; set; } = 0;
    }
}