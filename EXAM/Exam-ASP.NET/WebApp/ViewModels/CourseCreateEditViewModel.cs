using System;
using Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class CourseCreateEditViewModel
    {
        public Course Course { get; set; } = default!;
        public SelectList? TeacherSelectList { get; set; } = default!;
    }
}