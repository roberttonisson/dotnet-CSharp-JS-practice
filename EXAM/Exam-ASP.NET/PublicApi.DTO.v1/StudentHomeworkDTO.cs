using System;

namespace PublicApi.DTO.v1
{
    public class StudentHomeworkDTO
    {
        public string? Id { get; set; }

        public UserDTO? AppUser { get; set; }
        public string? AppUserId { get; set; }

        public HomeworkDTO? Homework { get; set; }
        public string? Grade { get; set; } = default!;
        public DateTime? GradedAt { get; set; }
        public string? StudentAnswer { get; set; }
    }
}