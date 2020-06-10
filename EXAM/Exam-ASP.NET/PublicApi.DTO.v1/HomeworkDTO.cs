using System;
using System.Collections.Generic;
using Domain;

namespace PublicApi.DTO.v1
{
    public class HomeworkDTO
    {
        public string Id { get; set; }

        public string? CourseId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        
        public DateTime? Deadline { get; set; }

        public ICollection<StudentHomeworkDTO>? StudentHomework { get; set; }
    }
}