﻿using System;
 using System.Collections;
 using System.Collections.Generic;
 using System.ComponentModel.DataAnnotations;
 using Microsoft.AspNetCore.Identity;

 namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(128, ErrorMessage = "Maximum length is 128")]
        public string FirstName { get; set; } = default!;

        [MinLength(1, ErrorMessage = "Minimum length is 1")]
        [MaxLength(128, ErrorMessage = "Maximum length is 128")] 
        public string LastName { get; set; } = default!;
        

        public string FirstLastName => FirstName + " " + LastName;

        public ICollection<Course>? Courses { get; set; }
        public ICollection<StudentHomework>? StudentHomework { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; }

    }

}