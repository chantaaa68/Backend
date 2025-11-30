using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.user
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string UserHash { get; set; } = null!;
    }

    public class LoginResponse
    {
        public required int UserId { get; set; }

        public required int KakeiboId { get; set; }
    }
}
