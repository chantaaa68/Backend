using Backend.Annotation;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication.Context;

namespace Backend.Repository
{
    [Component]
    public class NewsletterRepository(AWSDbContext _dbContext)
    {
        private readonly AWSDbContext dbContext = _dbContext;

        /// <summary>
        /// テンプレートの登録
        /// </summary>
        /// <param name="newsletter"></param>
        public async Task<int> ResistNewsletterTemplateAsync(NewsletterTemplate newsletter)
        {
            await this.dbContext.NewsletterTemplate.AddAsync(newsletter);

            return await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// テンプレートの更新
        /// </summary>
        /// <param name="newsletter"></param>
        public async Task<int> UpdsateNewsletterTemplateAsync(NewsletterTemplate newsletter)
        {
            this.dbContext.NewsletterTemplate.Update(newsletter);

            return await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// メールテンプレートを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<NewsletterTemplate?> GetNewsletterById(int id)
        {
            return await this.dbContext.NewsletterTemplate.Where(n => n.Id == id && n.DeleteDate == null).FirstOrDefaultAsync();
        }
    }
}
