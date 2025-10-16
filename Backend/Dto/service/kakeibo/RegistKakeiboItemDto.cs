using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service
{
    public class RegistKakeiboItemRequest
    {
        /**
         * カテゴリID
         */
        [Required]
        public int CategoryId { get; set; }

        /**
         * アイテム名
         */
        [Required]
        public string? ItemName { get; set; }

        /**
         * 金額
         */
        [Required]
        public int ItemAmount { get; set; }

        /**
         * 出入金フラグ
         */
        [Required]
        public bool InoutFlg { get; set; }

        /**
         * 出入金日付
         */
        [Required]
        public DateTime UsedDate { get; set; }
    }

    public class RegistKakeiboItemResponse
    {
        public required int Count { get; set; }
    }
}
