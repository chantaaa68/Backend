using Backend.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication.Model;

namespace Backend.Model
{
    public class KakeiboItemOption : KakeiboInterface
    {
        public KakeiboItemOption()
        {
            KakeiboItems = new HashSet<KakeiboItem>();
        }

        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("家計簿ID")]
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
        [Comment("固定費頻度")]
        public required int Frequency { get; set; }

        [Required]
        [Comment("固定費開始日時")]
        [MaxLength(20)]
        public required DateTime FixedStartDate { get; set; }

        [Required]
        [Comment("固定費終了日時")]
        [MaxLength(20)]
        public required DateTime FixedEndDate { get; set; }

        [Required]
        [Comment("登録日時")]
        public required DateTime CreateDate { get; set; }

        [Required]
        [Comment("更新日時")]
        public required DateTime UpdateDate { get; set; }

        [Comment("削除日時")]
        public DateTime? DeleteDate { get; set; }

        public virtual Kakeibo Kakeibo { get; set; } = null!;
        public virtual ICollection<KakeiboItem> KakeiboItems { get; set; }
    }
}
