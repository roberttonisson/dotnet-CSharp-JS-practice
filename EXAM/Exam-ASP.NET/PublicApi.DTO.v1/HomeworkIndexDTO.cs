using System.Collections.Generic;
using Domain;

namespace PublicApi.DTO.v1
{
    public class HomeworkIndexDTO
    {
        public IEnumerable<HomeworkDTO> HomeworkDtos { get; set; } = default!;
        public CourseDTO CourseDTO { get; set; } = default!;
    }
}