using Backend.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model
{
    public class KakeiboItem : KakeiboInterface
    {
        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("家計簿テーブルID")]
        [ForeignKey(nameof(Kakeibo))]
        public required int KakeiboId { get; set; }

        [Required]
        [Comment("カテゴリID")]
        [ForeignKey(nameof(Category))]
        public required int CategoryId { get; set; }

        [Required]
        [Comment("名前")]
        [MaxLength(20)]
        public required string ItemName { get; set; }

        [Required]
        [Comment("金額")]
        public required int ItemAmount { get; set; }

        [Required]
        [Comment("出入金フラグ")]
        public required bool InoutFlg { get; set; }

        [Required]
        [Comment("出入金日付")]
        public required DateTime UsedDate { get; set; }

        [Required]
        [Comment("オプションID")]
        [ForeignKey(nameof(KakeiboItemOption))]
        public required int ItemOptionId { get; set; }

        [Required]
        [Comment("登録日時")]
        public required DateTime CreateDate { get; set; }

        [Required]
        [Comment("更新日時")]
        public required DateTime UpdateDate { get; set; }

        [Comment("削除日時")]
        public DateTime? DeleteDate { get; set; }

        public virtual required Kakeibo Kakeibo { get; set; } = null!;

        public virtual required Category Category { get; set; } = null!;

        public virtual required KakeiboItemOption KakeiboItemOption { get; set; } = null!;

    }
}
