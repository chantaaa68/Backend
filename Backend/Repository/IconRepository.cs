using Backend.Annotation;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication.Context;

namespace Backend.Repository
{
    [AutoDI]
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
        public async Task<int?> GetIconIdAsync(string officialIconName)
        {
            Icon? icon = await this.dbContext.Icon
                .Where(i => i.OfficialIconName == officialIconName && i.DeleteDate == null)
                .FirstOrDefaultAsync();

            if (icon == null)
            {
                return null;
            }
            else
            {
                return icon.Id;
            }
        }
    }
}
