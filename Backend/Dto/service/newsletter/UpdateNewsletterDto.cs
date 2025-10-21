namespace Backend.Dto.service.newsletter
{
    public class UpdateNewsletterRequest
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string MailBody { get; set; }
    }

    public class UpdateNewsletterResponse
    {
        public required int Count { get; set; }
    }
}
