using Backend.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Icon : KakeiboInterface
    {
        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("アイコン名")]
        public required string OfficialIconName { get; set; }

        [Required]
        [Comment("表示名")]
        public required string DefalultIconName { get; set; }

        [Required]
        [Comment("登録日時")]
        public required DateTime CreateDate { get; set; }

        [Required]
        [Comment("更新日時")]
        public required DateTime UpdateDate { get; set; }

        [Comment("削除日時")]
        public DateTime? DeleteDate { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
