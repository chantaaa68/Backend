using Backend.Annotation;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication.Context;

namespace Backend.Repository
{
    [Component]
    public class IconRepository(AWSDbContext _dbContext)
    {
        private readonly AWSDbContext dbContext = _dbContext;

        /// <summary>
        /// DBに登録されているアイコンを全部取得する
        /// </summary>
        /// <returns></returns>
        public async Task<List<Icon>> GetIconListAsync()
        {
            // Iconテーブルからデータを取得
            return await this.dbContext.Icon.Where(i => i.DeleteDate == null).ToListAsync();
        }

        /// <summary>
        /// アイコン名からアイコンIDを取得する
        /// </summary>
        /// <param name="defaultIconName"></param>
        /// <returns></returns>
        public async Task<int> GetIconIdAsync(string defaultIconName)
        {
            Icon icon = await this.dbContext.Icon
                .Where(i => i.DefaultIconName == defaultIconName && i.DeleteDate == null)
                .FirstAsync();
            
            return icon.Id;
        }
    }
}
