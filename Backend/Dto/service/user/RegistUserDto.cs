using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.user
{
    public class RegistUserRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string UserHash { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string KakeiboName { get; set; } = null!;
        [Required]
        public string KakeiboExplanation { get; set; } = null!;
    }

    public class RegistUserResponse
    {
        public required int UserId { get; set; }
    }
}
