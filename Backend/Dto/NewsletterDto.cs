namespace Backend.Dto
{
    public class NewsletterDto
    {
        public class NewsletterRigistRequest
        {
            public required string Title { get; set; }
            public required string Content { get; set; }
        }

        public class NewsletterRigistResponse
        {
            public int Id { get; set; }
        }

        public class NewsletterSendRequest
        {
            public int NewsletterId { get; set; }

            public int UserId { get; set; }
        }

        public class NewsletterSendResponse
        {
            public int sendCount { get; set; }

        }
    }
}
