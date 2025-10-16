using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service
{
    public class UpdateKakeiboItemRequest
    {
        /**
         * アイテムID
         */
        [Required]
        public int Id { get; set; }

        /**
         * カテゴリID
         */
        public int? CategoryId { get; set; }

        /**
         * アイテム名
         */
        public string? ItemName { get; set; }

        /**
         * 金額
         */
        public int? ItemAmount { get; set; }

        /**
         * 出入金フラグ
         */
        public bool? InoutFlg { get; set; }

        /**
         * 出入金日付
         */
        public DateTime? UsedDate { get; set; }
    }

    public class UpdateKakeiboItemResponse
    {
        public required int Count { get; set; }
    }
}
