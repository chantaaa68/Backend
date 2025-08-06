using Backend.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model
{
    public class Category : KakeiboInterface
    {
        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("家計簿テーブルID")]
        [ForeignKey(nameof(Kakeibo))]
        public required int KakeiboID { get; set; }

        [Required]
        [Comment("カテゴリ名")]
        [MaxLength(20)]
        public required string KategoryName { get; set; }

        [Required]
        [Comment("出入金フラグ")]
        public required Boolean InoutFlg { get; set; }

        [Required]
        [Comment("アイコンID")]
        [ForeignKey(nameof(Icon))]
        public required int IconId { get; set; }

        [Required]
        [Comment("登録日時")]
        public required DateTime CreateDate { get; set; }

        [Required]
        [Comment("更新日時")]
        public required DateTime UpdateDate { get; set; }

        [Comment("削除日時")]
        public DateTime? DeleteDate { get; set; }

        public virtual required Kakeibo Kakeibo { get; set; } = null!;

        public virtual required Icon Icon { get; set; } = null!;
    }
}
