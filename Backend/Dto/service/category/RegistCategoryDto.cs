using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.category
{
    public class RegistCategoryRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public bool InoutFlg { get; set; }
        [Required]
        public string IconName { get; set; } = string.Empty;
    }

    public class RegistCategoryResponse
    {
        public required int CategoryId { get; set; }
    }
}
