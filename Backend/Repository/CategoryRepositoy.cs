using Backend.Annotation;
using Backend.Dto.common;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication.Context;

namespace Backend.Repository
{
    [AutoDI]
    public class CategoryRepositoy(AWSDbContext _dbContext)
    {
        public readonly AWSDbContext dbContext = _dbContext;


        /// <summary>
        /// デフォルトで登録されているカテゴリを取得する
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryItem>> GetCategoryDefaultAsync() 
        {
            List<CategoryDefault> categories = await this.dbContext.CategoryDefault
                .Where(c => c.DeleteDate == null)
                .Include(c => c.Icon)
                .ToListAsync();

            List<CategoryItem> result = categories.Select(c => new CategoryItem
            {
                Id = c.Id,
                CategoryName = c.KategoryName,
                InoutFlg = c.InoutFlg,
                IconName = c.Icon.OfficialIconName
            }).ToList();

            return result;
        }

        /// <summary>
        /// ユーザー登録カテゴリを取得する
        /// </summary>
        /// <returns></returns>
        public async Task<List<CategoryItem>> GetCategoryAsync(int kakeiboId)
        {
            List<Category> categories = await this.dbContext.Category
                .Where(c => c.KakeiboID == kakeiboId && c.DeleteDate == null)
                .Include(c => c.Icon)
                .ToListAsync();

            List<CategoryItem> result = categories.Select(c => new CategoryItem
                {
                    Id = c.Id,
                    CategoryName = c.CategoryName,
                    InoutFlg = c.InoutFlg,
                    IconName = c.Icon.OfficialIconName
                }).ToList();

            return result;
        }

        /// <summary>
        /// ユーザーカテゴリを一件取得する
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            return await this.dbContext.Category.FindAsync(categoryId);
        }

        /// <summary>
        /// ユーザー登録カテゴリを登録する
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<int> RegistCategoryAsync(Category category)
        {
            await this.dbContext.Category.AddAsync(category);

            return await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// ユーザーカテゴリの更新を実施する
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<int> UpdateCategoryAsync(Category category)
        {
            this.dbContext.Category.Update(category);

            return await this.dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
