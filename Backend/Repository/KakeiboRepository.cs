using Backend.Annotation;
using Backend.Dto.common;
using Backend.Dto.repository;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication.Context;

namespace Backend.Repository
{
    [AutoDI]
    public class KakeiboRepository(AWSDbContext dbContext)
    {
        private readonly AWSDbContext _dbContext = dbContext;

        /// <summary>
        /// ユーザーIDから家計簿IDを取得する
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int?> GetKakeiboIdAsync(int userId)
        {
            //これを呼び出すとき、家計簿は必ず存在する
            Kakeibo? kakeibo = await this._dbContext.Kakeibo
                .Where(k => k.UserId == userId && k.DeleteDate == null).FirstOrDefaultAsync();

            if (kakeibo == null) 
            {
                return null;
            }
            else
            {
                return kakeibo.Id;
            }
        }

        /// <summary>
        /// 家計簿情報を取得する
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Kakeibo?> GetKakeiboAsync(int userId)
        {
            Kakeibo? result = await this._dbContext.Kakeibo
                .Where(k => k.UserId == userId && k.DeleteDate == null).FirstOrDefaultAsync();

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
                        MonthlyReportItems = i.GroupBy(e => new { e.UsedDate.Year, e.UsedDate.Month })
                            .Select(t => new MonthlyReportItem
                            {
                                UsedMonth = $"{t.Key.Year }-{t.Key.Month}",
                                CategoryReportItems = t.GroupBy(t => t.Category.CategoryName)
                                    .Select(e => new CategoryReportItem
                                    {
                                        CategoryName = e.Key,
                                        IconName = e.First().Category.Icon.OfficialIconName,
                                        TotalAmount = e.Sum(e => e.ItemAmount)
                                    })
                                    .ToList()
                            })
                            .ToList()
                    }).ToListAsync()
            };

            return result;
        }

        /// <summary>
        /// その月のアイテムデータ一覧を取得する
        /// </summary>
        /// <param name="kakeiboId"></param>
        /// <returns></returns>
        public async Task<GetKakeiboItemListResult> GetKakeiboItemListAsync(GetKakeiboItemListParameter prm)
        {

            List<KakeiboItemInfo> kakeiboItemInfos = new();

            await this._dbContext.KakeiboItem.Where(k => k.KakeiboId == prm.KakeiboId 
                    && prm.StartDate <= k.UsedDate 
                    && k.UsedDate <= prm.EndDate
                    && k.DeleteDate == null)
                .Include(k => k.Category)
                    .ThenInclude(c => c.Icon)
                .GroupBy(k => k.UsedDate.Day)
                .ForEachAsync(dayGroup =>
                {
                    List<Item> items = new();

                    foreach (var days in dayGroup)
                    {
                        Item item = new()
                        {
                            ItemId = days.Id,
                            ItemName = days.ItemName,
                            ItemAmount = days.ItemAmount,
                            InoutFlg = days.InoutFlg,
                            UsedDate = days.UsedDate,
                            IconName = days.Category.Icon.OfficialIconName
                        };

                        items.Add(item);
                    }
                    

                    KakeiboItemInfo info = new()
                    {
                        DayNo = dayGroup.Key,
                        Items = items
                    };
                    kakeiboItemInfos.Add(info);
                });

            GetKakeiboItemListResult result = new()
            {
                KakeiboItemInfos = kakeiboItemInfos.OrderBy(i => i.DayNo).ToList()
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
        /// アイテムデータリストの取得
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<KakeiboItem>?> GetFrequencyKakeiboItemListAsync(GetFrequencyKakeiboItemListParameter prm)
        {
            List<KakeiboItem>? result = await this._dbContext.KakeiboItem
                .Where(k => k.FrequencyId == prm.frequencyId 
                    && prm.StartDate <= k.UsedDate
                    && k.DeleteDate == null
                    )
                .Include(k => k.Category)
                .ToListAsync();

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
        /// アイテムの一括登録を行う
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<int> RegistKakeiboItemListAsync(List<KakeiboItem> items)
        {
            await this._dbContext.KakeiboItem.AddRangeAsync(items);

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

        /// <summary>
        /// 一括更新・削除を行う
        /// </summary>
        /// <param name="prm"></param>
        /// <returns></returns>
        public async Task<int> UpdateKakeiboItemListAsync(List<KakeiboItem> prm)
        {
            this._dbContext.KakeiboItem.UpdateRange(prm);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 固定費管理データを取得する
        /// </summary>
        /// <param name="frequencyId"></param>
        /// <returns></returns>
        public async Task<KakeiboItemFrequency?> GetKakeiboItemFrequencyAsync(int frequencyId)
        {
            KakeiboItemFrequency? frequency = await this._dbContext.KakeiboItemFrequency
                .Where(e => e.Id == frequencyId && e.DeleteDate == null)
                .FirstOrDefaultAsync();

            return frequency;
        }

        /// <summary>
        /// 固定費管理テーブルに登録する
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public async Task<int> RegistKakeiboItemFrequencyAsync(KakeiboItemFrequency frequency)
        {
            await this._dbContext.KakeiboItemFrequency.AddAsync(frequency);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 固定費管理テーブルを更新する
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public async Task<int> UpdateKakeiboItemFrequencyAsync(KakeiboItemFrequency frequency)
        {
            this._dbContext.KakeiboItemFrequency.Update(frequency);

            return await this._dbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
