using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.newsletter
{
    public class SendNewsletterRequest
    {
        /// <summary>
        /// メールテンプレートID
        /// </summary>
        [Required]
        public int NewsletterId { get; set; }

        /// <summary>
        /// 送信先のユーザーID
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// アイテムID
        /// </summary>
        [Required]
        public int ItemId { get; set; }
    }

    public class SendNewsletterResponse
    {
        public int SendCount { get; set; }

    }
}
