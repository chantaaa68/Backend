using Backend.Model;
using WebApplication.Context;

namespace Backend.Repository
{
    public class NewsletterRepository
    {
        private readonly AWSDbContext _dbContext;

        public NewsletterRepository(AWSDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void NewsletterResist(NewsletterTemplate newsletter)
        {
            this._dbContext.NewsletterTemplates.Add(newsletter);

            this._dbContext.SaveChanges();
        }

        public NewsletterTemplate GetNewsletterById(int id)
        {
            return this._dbContext.NewsletterTemplates.FirstOrDefault(n => n.Id == id) 
                   ?? throw new KeyNotFoundException($"Newsletter with ID {id} not found.");
        }
    }
}
