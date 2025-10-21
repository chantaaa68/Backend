using System.ComponentModel.DataAnnotations;

namespace Backend.Model.Interface
{
    public interface KakeiboInterface
    {
        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}
