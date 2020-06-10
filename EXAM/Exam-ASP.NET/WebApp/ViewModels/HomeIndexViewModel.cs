using System.Collections.Generic;
using Domain;

namespace WebApp.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<StudentHomework> StudentHomework = new List<StudentHomework>();
        public IEnumerable<StudentCourse>? StudentCourses = new List<StudentCourse>();
    }
}