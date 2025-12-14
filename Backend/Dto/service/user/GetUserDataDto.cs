namespace Backend.Dto.service.user
{
    public class GetUserDataRequest
    {
        public required int UserId { get; set; }
    }

    public class GetUserDataResponse
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string KakeiboName { get; set; } = null!;

        public string? KakeiboExplanation { get; set; }
    }
}
