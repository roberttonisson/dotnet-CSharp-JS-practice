using System;
using System.Collections;
using System.Collections.Generic;
using Domain;

namespace WebApp.ViewModels
{
    public class HomeworkIndexViewModel
    {
        public IEnumerable<Homework> Homework { get; set; }
        public Course Course { get; set; } = default!;
    }
}