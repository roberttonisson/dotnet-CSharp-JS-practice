using System.Collections.Generic;
using Domain;

namespace PublicApi.DTO.v1
{
    public class CourseDTO
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public int ECTS { get; set; } = default!;
        public string Semester { get; set; } = default!;
        public int Year { get; set; } = default!;
        public string Description { get; set; } = default!;
        public UserDTO? User { get; set; }

        public ICollection<HomeworkDTO>? Homework { get; set; }
        public ICollection<StudentCourseDTO>? StudentCourses { get; set; }
    }
}