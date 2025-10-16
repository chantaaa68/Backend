using Backend.Annotation;
using Backend.Dto.common;
using Backend.Dto.repository;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication.Context;

namespace Backend.Repository
{
    [Component]
    public class KakeiboRepository(AWSDbContext dbContext)
    {
        private readonly AWSDbContext _dbContext = dbContext;

        /// <summary>
        /// ユーザーIDから家計簿IDを取得する
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetKakeiboIdAsync(int userId)
        {
            //これを呼び出すとき、家計簿は必ず存在する
            Kakeibo kakeibo = await this._dbContext.Kakeibo
                .Where(k => k.UserId == userId && k.DeleteDate == null).FirstAsync();

            return kakeibo.Id;
        }

        /// <summary>
        /// 家計簿情報を取得する
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Kakeibo> GetKakeiboAsync(int userId)
        {
            Kakeibo result = await this._dbContext.Kakeibo
                .Where(k => k.UserId == userId && k.DeleteDate == null).FirstAsync();

            return result;
        }

        /// <summary>
        /// 家計簿の登録（返却値は家計簿ID）
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public async Task<int> RegistKakeiboAsync(Kakeibo prm)
        {
            await this._dbContext.Kakeibo.AddAsync(prm);
            await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

            return prm.Id;
        }

        /// <summary>
        /// 家計簿の更新を行う
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public async Task<int> UpdateKakeiboAsync(Kakeibo prm)
        {
            Kakeibo kakeibo = await this._dbContext.Kakeibo.Where(k => k.Id == prm.Id && k.DeleteDate == null).FirstAsync();

            kakeibo.KakeiboName = prm.KakeiboName!;
            kakeibo.KakeiboExplanation = prm.KakeiboExplanation;
            kakeibo.UpdateDate = DateTime.Now;

            this._dbContext.Kakeibo.Update(kakeibo);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 月別集計データの取得
        /// </summary>
        /// <param name="kakeiboId"></param>
        /// <returns></returns>
        public async Task<GetMonthlyReportResult> GetMonthlyReportAsync(int kakeiboId)
        {
            GetMonthlyReportResult result = new()
            {
                MonthlyReports = await this._dbContext.KakeiboItem
                    .Where(k => k.KakeiboId == kakeiboId && k.DeleteDate == null)
                    .Include(k => k.Category)
                        .ThenInclude(c => c.Icon)
                    .GroupBy(k => k.InoutFlg)
                    .Select(i => new MonthlyReport
                    {
                        InoutFlg = i.Key,
                        MonthlyReportItems = i.GroupBy(i => i.UsedDate.ToString("yyyy-MM"))
                            .Select(t => new MonthlyReportItem
                            {
                                UsedMonth = t.Key,
                                CategoryReportItems = t.GroupBy(t => t.Category.CategoryName)
                                    .Select(e => new CategoryReportItem
                                    {
                                        CategoryName = e.Key,
                                        TotalAmount = e.Sum(e => e.ItemAmount)
                                    })
                                    .ToList()
                            })
                            .OrderBy(m => m.UsedMonth)
                            .ToList()
                    }).ToListAsync()
            };

            return result;
        }

        /// <summary>
        /// 個別アイテムデータの取得
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<KakeiboItem?> GetKakeiboItemAsync(int itemId)
        {
            KakeiboItem? result = await this._dbContext.KakeiboItem
                .Where(k => k.Id == itemId && k.DeleteDate == null)
                .Include(k => k.Category)
                    .ThenInclude(c => c.Icon)
                .FirstOrDefaultAsync();

            return result;
        }


        /// <summary>
        /// アイテムの登録を行う
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> RegistKakeiboItemAsync(KakeiboItem item)
        {
            await this._dbContext.KakeiboItem.AddAsync(item);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 更新・削除を行う
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public async Task<int> UpdateKakeiboItemAsync(KakeiboItem prm)
        {
            this._dbContext.KakeiboItem.Update(prm);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
