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

        public void NewsletterResist(Newsletter newsletter)
        {
            this._dbContext.Newsletters.Add(newsletter);

            this._dbContext.SaveChanges();
        }

        public Newsletter GetNewsletterById(int id)
        {
            return this._dbContext.Newsletters.FirstOrDefault(n => n.Id == id) 
                   ?? throw new KeyNotFoundException($"Newsletter with ID {id} not found.");
        }
    }
}
