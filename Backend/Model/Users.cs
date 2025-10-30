﻿using Backend.Model;
using Backend.Model.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Model
{
    public class Users : KakeiboInterface
    {
        [Key]
        [Comment("ID")]
        public int Id { get; set; }

        [Comment("ユーザーHash")]
        public required string UserHash { get; set; }

        [Comment("ユーザー名")]
        public required string Name { get; set; } = null!;

        [Comment("メールアドレス")]
        public required string Email { get; set; } = null!;

        [Comment("登録日時")]
        public DateTime CreateDate { get; set; }

        [Comment("更新日時")] 
        public DateTime UpdateDate { get; set; }

        [Comment("削除日時")]
        public DateTime? DeleteDate { get; set; }

        public virtual Kakeibo Kakeibo { get; set; } = null!;
    }
}
