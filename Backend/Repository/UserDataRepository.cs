using Backend.Annotation;
using Microsoft.EntityFrameworkCore;
using WebApplication.Context;
using WebApplication.Model;

namespace WebApplication.Repository
{
    [AutoDI]
    public class UserDataRepository(AWSDbContext dbContext)
    {
        private readonly AWSDbContext _dbContext = dbContext;

        /// <summary>
        /// ログインユーザーを取得する
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userHash"></param>
        /// <returns></returns>
        public async Task<Users?> GetLoginUserAsync(string email, string userHash)
        {
            return await this._dbContext.Users.Where(u => u.Email.Equals(email) && u.UserHash.Equals(userHash))
                                              .Include(u => u.Kakeibo)
                                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// IDから対象のユーザーと家計簿のデータを取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Users?> GetUserAsync(int id)
        {
            return await this._dbContext.Users.Where(u => u.Id == id && u.DeleteDate == null)
                                              .Include(u => u.Kakeibo)
                                              .FirstOrDefaultAsync();
        }

        /// <summary>
        /// ユーザー登録を実施する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UserRigistAsync(Users request)
        {
            this._dbContext.Users.Add(request);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// ユーザーの更新を実施する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateUserAsync(Users request)
        {
            this._dbContext.Users.Add(request);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// ユーザーIDからユーザー情報だけを取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Users? GetUserById(int id)
        {
            return this._dbContext.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
