using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service
{
    public class RegistKakeiboItemRequest
    {

        /**
         * 家計簿ID
         */
        [Required]
        public int KakeiboId { get; set; }

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

        /**
         * カテゴリID
         */
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// 固定費頻度
        /// </summary>
        [Range(0, 11)]
        [Required]
        public int Frequency { get; set; }

        /// <summary>
        /// 固定費終了日付
        /// </summary>
        public DateTime? FixedEndDate { get; set; }
    }

    public class RegistKakeiboItemResponse
    {
        public required int Count { get; set; }
    }
}
