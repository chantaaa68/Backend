using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.user
{
    public class UpdateUserRequest
    {
        public required int UserId { get; set; }
        
        public string? UserName { get; set; }
        
        public string? Email { get; set; }
        
        public string? KakeiboName { get; set; }
        
        public string? KakeiboExplanation { get; set; }
    }

    public class UpdateUserResponse
    {
        public required int UserId { get; set; }
    }
}
