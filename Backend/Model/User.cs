using Backend.Model;
using Backend.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Model
{
    public class User : KakeiboInterface
    {
        public User()
        {
            KakeiboItemOptions = new HashSet<KakeiboItemOption>();
        }

        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("ユーザーHash")]
        public required string UserHash { get; set; }

        [Required]
        [Comment("ユーザー名")]
        public required string Name { get; set; }

        [Required]
        [Comment("メールアドレス")]
        public required string Email { get; set; }

        [Required]
        [Comment("登録日時")]
        public required DateTime CreateDate { get; set; }

        [Required]
        [Comment("更新日時")] 
        public  required DateTime UpdateDate { get; set; }

        [Comment("削除日時")]
        public DateTime? DeleteDate { get; set; }

        public virtual Kakeibo Kakeibo { get; set; } = null!;
        public virtual ICollection<KakeiboItemOption> KakeiboItemOptions { get; set; }

    }
}
