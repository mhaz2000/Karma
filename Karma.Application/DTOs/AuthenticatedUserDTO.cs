namespace Karma.Application.DTOs
{
    public record AuthenticatedUserDTO
    {
        public required string AuthToken { get; set; }
        public required string RefreshToken { get; set; }
        public bool IsAdmin { get; set; }
    }
}
