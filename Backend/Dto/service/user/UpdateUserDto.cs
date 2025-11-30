using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.user
{
    public class UpdateUserRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? KakeiboName { get; set; }
        [Required]
        public string? KakeiboExplanation { get; set; }
    }

    public class UpdateUserResponse
    {
        public required int UserId { get; set; }
    }
}
