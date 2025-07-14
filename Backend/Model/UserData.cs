using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Model
{
    public class UserData
    {
        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("ユーザー名")]
        public required string UserName { get; set; }

        [Required]
        [Comment("メールアドレス")]
        public required string Email { get; set; }

    }
}
