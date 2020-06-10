using System.Collections.Generic;
using Domain;

namespace PublicApi.DTO.v1
{
    public class HomeDTO
    {
        public IEnumerable<StudentCourseDTO> StudentCourses = new List<StudentCourseDTO>();
    }
}