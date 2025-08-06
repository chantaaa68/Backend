using Backend.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication.Model;

namespace Backend.Model
{
    public class Kakeibo : KakeiboInterface
    {
        public Kakeibo()
        {
            KakeiboItems = new HashSet<KakeiboItem>();
            Categories = new HashSet<Category>();
        }

        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("ユーザーID")]
        [ForeignKey(nameof(User))]
        public required int UserId { get; set; }

        [Required]
        [Comment("家計簿名")]
        [MaxLength(20)]
        public required string KakeiboName { get; set; }

        [Comment("説明文")]
        [MaxLength(1024)]
        public string? KakeiboExplanation { get; set; }

        [Required]
        [Comment("登録日時")]
        public required DateTime CreateDate { get; set; }

        [Required]
        [Comment("更新日時")]
        public required DateTime UpdateDate { get; set; }

        [Comment("削除日時")]
        public DateTime? DeleteDate { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<KakeiboItem> KakeiboItems { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
