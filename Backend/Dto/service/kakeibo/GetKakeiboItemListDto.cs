using Backend.Dto.repository;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.kakeibo
{
    public class GetKakeiboItemListRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string? Range { get; set; }
    }

    public class GetKakeiboItemListResponse
    {
        public required List<KakeiboItemInfo> KakeiboItemInfos { get; set; }
    }
}
