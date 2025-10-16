using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.kakeibo
{
    public class GetKakeiboItemDetailRequest
    {
        [Required]
        public int ItemId { get; set; }
    }

    public class GetKakeiboItemDetailResponse
    {
        public required string ItemName { get; set; }

        public required int ItemAmount { get; set; }

        public required bool InoutFlg { get; set; }

        public required DateTime UsedDate { get; set; }

        public required int CategoryId { get; set; }

    }
}
