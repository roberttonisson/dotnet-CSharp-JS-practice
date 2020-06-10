namespace PublicApi.DTO.v1
{
    public class RegisterDTO
    {
        public string Email { get; set; } = default!;
        public string? Password { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }
}