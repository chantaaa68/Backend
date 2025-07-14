using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public class Newsletter
    {
        [Key]
        [Required]
        [Comment("ID")]
        public int Id { get; set; }

        [Required]
        [Comment("メールタイトル")]
        public required string Title { get; set; }

        [Required]
        [Comment("メールの中身")]
        public required string Content { get; set; }

    }
}
