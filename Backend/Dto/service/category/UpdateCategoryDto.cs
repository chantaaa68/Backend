using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.category
{
    public class UpdateCategoryRequest
    {
        /// <summary>
        /// カテゴリID
        /// </summary>
        [Required]
        public int Id { get; set; }
        
        public string? CategoryName { get; set; }
        
        public bool? InoutFlg { get; set; }
        
        public string? IconName { get; set; }
    }

    public class UpdateCategoryResponse
    {
        public required int CategoryId { get; set; }
    }
}
