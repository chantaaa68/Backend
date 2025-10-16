using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service
{
    public class UpdateKakeiboRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? KakeiboName { get; set; }

        [Required]
        public string? KakeiboExplanation { get; set; }
    }

    public class UpdateKakeiboResponse
    {
        public required int Count { get; set; }
    }
}
