namespace Karma.Application.DTOs
{
    public record AuthenticatedUserDTO
    {
        public required string AuthToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}
