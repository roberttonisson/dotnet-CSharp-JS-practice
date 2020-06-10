namespace PublicApi.DTO.v1
{
    public class StudentCourseDTO
    {
        public string Id { get; set; } = default!;
        public UserDTO? AppUser { get; set; } = default!;
        public string? AppUserId { get; set; } = default!;
        public CourseDTO? Course { get; set; }
        public bool? Accepted { get; set; }
        public string? Grade { get; set; }
    }
}