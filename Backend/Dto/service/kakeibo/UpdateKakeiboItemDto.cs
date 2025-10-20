using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service
{
    public class UpdateKakeiboItemRequest
    {
        /**
         * アイテムID
         */
        [Required]
        public int ItemId { get; set; }

        /**
         * カテゴリID
         */
        [Required]
        public int CategoryId { get; set; }

        /**
         * アイテム名
         */
        [Required]
        public string ItemName { get; set; }

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

        /// <summary>
        /// 単体変更判断フラグ(true：該当アイテムのみ, false：それ以降全部)
        /// </summary>
        [Required]
        public bool ChangeFlg { get; set; }
    }

    public class UpdateKakeiboItemResponse
    {
        public required int Count { get; set; }
    }
}
