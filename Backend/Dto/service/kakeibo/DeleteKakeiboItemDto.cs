using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service
{
    public class DeleteKakeiboItemRequest
    {
        /**
         * アイテムID
         */
        [Required]
        public int Id { get; set; }
    }

    public class DeleteKakeiboItemResponse
    {
        public required int Count { get; set; }
    }
}
