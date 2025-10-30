namespace Backend.Dto.service.newsletter
{
    public class RigistNewsletterRequest
    {
        public required string Title { get; set; }
        public required string MailBody { get; set; }
    }

    public class RigistNewsletterResponse
    {
        public int Count { get; set; }
    }
}
