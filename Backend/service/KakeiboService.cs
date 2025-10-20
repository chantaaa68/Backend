using Backend.Annotation;
using Backend.Dto.common;
using Backend.Dto.repository;
using Backend.Dto.service;
using Backend.Dto.service.kakeibo;
using Backend.Model;
using Backend.Repository;
using WebApplication.Context;
using static Backend.Utility.Enums;

namespace Backend.service
{
    [Component]
    public class KakeiboService(KakeiboRepository _kakeiboRepositoy,AWSDbContext _dbContext)
    {

        public KakeiboRepository kakeiboRepositoy = _kakeiboRepositoy;

        public AWSDbContext dbContext = _dbContext;


        /// <summary>
        /// 月間レポートの取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<GetMonthlyResultResponse> GetMonthlyResultAsync(GetMonthlyResultRequest req)
        {
            // ユーザーIDから家計簿IDを取得
            int kakeiboId = await this.kakeiboRepositoy.GetKakeiboIdAsync(req.UserId);

            // 月次レポートを取得する
            GetMonthlyReportResult result = await this.kakeiboRepositoy.GetMonthlyReportAsync(kakeiboId);

            // 支出と収入で分ける(InoutFlgがtrue:収入、false:支出)
            GetMonthlyResultResponse response = new()
            {
                MonthlyExpenses = result.MonthlyReports.FirstOrDefault(m => !m.InoutFlg).MonthlyReportItems ?? new List<MonthlyReportItem>(),
                MonthlyIncomes = result.MonthlyReports.FirstOrDefault(m => m.InoutFlg).MonthlyReportItems ?? new List<MonthlyReportItem>(),
            };

            return response;

        }

        /// <summary>
        /// アイテム詳細の取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<GetKakeiboItemDetailResponse> GetKakeiboItemDetailAsync(GetKakeiboItemDetailRequest req)
        {
            KakeiboItem? item = await this.kakeiboRepositoy.GetKakeiboItemAsync(req.ItemId);

            if (item == null)
            {
                throw new Exception("アイテムがありません");
            }
            else
            {
                GetKakeiboItemDetailResponse response = new()
                {
                    ItemName = item.ItemName!,
                    ItemAmount = item.ItemAmount,
                    InoutFlg = item.InoutFlg,
                    UsedDate = item.UsedDate,
                    CategoryId = item.CategoryId
                };

                return response;
            }
        }

        /// <summary>
        /// 家計簿の更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<UpdateKakeiboResponse> UpdateKakeiboAsync(UpdateKakeiboRequest req)
        {

            Kakeibo kakeibo = await this.kakeiboRepositoy.GetKakeiboAsync(req.Id);

            kakeibo.KakeiboName = req.KakeiboName ?? kakeibo.KakeiboName;
            kakeibo.KakeiboExplanation = req.KakeiboExplanation ?? kakeibo.KakeiboExplanation;

            UpdateKakeiboResponse response = new()
            {
                Count = await this.kakeiboRepositoy.UpdateKakeiboAsync(kakeibo)
            };

            return response;
        }

        /// <summary>
        /// アイテム登録
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<RegistKakeiboItemResponse> RegistKakeiboItemAsync(RegistKakeiboItemRequest req)
        {
            // くり返し処理用の出入金日付
            DateTime usedDate = req.UsedDate;

            // 繰り返し用日数or月数or年数を設定
            int frequencyNumber = req.Frequency switch
            {
                (int)Frequency.OnlyOnce => 0,
                (int)Frequency.EveryDay => 1,
                (int)Frequency.EveryWeek => 7,
                (int)Frequency.EveryTwoWeeks => 14,
                (int)Frequency.EveryThreeWeeks => 21,
                (int)Frequency.EveryMonth => 1,
                (int)Frequency.EveryTwoMonths => 2,
                (int)Frequency.EveryThreeMonths => 3,
                (int)Frequency.EveryFourMonths => 4,
                (int)Frequency.EveryFiveMonths => 5,
                (int)Frequency.EverySixMonths => 6,
                (int)Frequency.EveryYear => 1,
                _ => 0
            };

            // 登録するアイテムリスト
            List<KakeiboItem> items = new List<KakeiboItem>();

            // 固定費頻度によって処理を分岐
            if (req.Frequency == (int)Frequency.OnlyOnce) 
            {
                // 固定費管理テーブル登録用データの作成
                KakeiboItemFrequency frequency = new()
                {
                    KakeiboId = req.KakeiboId,
                    Frequency = req.Frequency,
                    CategoryId = req.CategoryId
                };

                // 固定費管理テーブルに登録を実施
                await this.kakeiboRepositoy.RegistKakeiboItemFrequencyAsync(frequency);

                KakeiboItem item = new KakeiboItem()
                {
                    KakeiboId = req.KakeiboId,
                    CategoryId = req.CategoryId,
                    ItemName = req.ItemName,
                    ItemAmount = req.ItemAmount,
                    InoutFlg = req.InoutFlg,
                    UsedDate = usedDate,
                    FrequencyId = frequency.Id,
                };

                // リストに追加
                items.Add(item);
            }
            else if(req.Frequency == (int)Frequency.EveryDay
                || req.Frequency == (int)Frequency.EveryWeek)
            {
                // 固定費管理テーブル登録用データの作成
                KakeiboItemFrequency frequency = new()
                {
                    KakeiboId = req.KakeiboId,
                    CategoryId = req.CategoryId,
                    ItemName = req.ItemName,
                    ItemAmount = req.ItemAmount,
                    InoutFlg = req.InoutFlg,
                    Frequency = req.Frequency,
                    FixedStartDate = req.UsedDate,
                    FixedEndDate = req.FixedEndDate,
                };

                // 固定費管理テーブルに登録を実施
                await this.kakeiboRepositoy.RegistKakeiboItemFrequencyAsync(frequency);

                // 繰り返し終了日付に到達するまで繰り返し
                while (req.FixedEndDate > usedDate)
                {
                    KakeiboItem item = new KakeiboItem()
                    {
                        KakeiboId = req.KakeiboId,
                        CategoryId = req.CategoryId,
                        ItemName = req.ItemName,
                        ItemAmount = req.ItemAmount,
                        InoutFlg = req.InoutFlg,
                        UsedDate = usedDate,
                        FrequencyId = frequency.Id,
                    };

                    // リストに追加
                    items.Add(item);

                    // 指定の日数進める
                    usedDate = usedDate.AddDays(frequencyNumber);
                }

            } 
            else if(req.Frequency == (int)Frequency.EveryMonth 
                || req.Frequency == (int)Frequency.EveryTwoMonths
                || req.Frequency == (int)Frequency.EveryThreeMonths
                || req.Frequency == (int)Frequency.EveryFourMonths
                || req.Frequency == (int)Frequency.EveryFiveMonths
                || req.Frequency == (int)Frequency.EverySixMonths)
            {
                // 固定費管理テーブル登録用データの作成
                KakeiboItemFrequency frequency = new()
                {
                    KakeiboId = req.KakeiboId,
                    CategoryId = req.CategoryId,
                    ItemName = req.ItemName,
                    ItemAmount = req.ItemAmount,
                    InoutFlg = req.InoutFlg,
                    Frequency = req.Frequency,
                    FixedStartDate = req.UsedDate,
                    FixedEndDate = req.FixedEndDate,
                };

                // 固定費管理テーブルに登録を実施
                await this.kakeiboRepositoy.RegistKakeiboItemFrequencyAsync(frequency);

                // 繰り返し終了日付に到達するまで繰り返し
                while (req.FixedEndDate > usedDate)
                {
                    KakeiboItem item = new KakeiboItem()
                    {
                        KakeiboId = req.KakeiboId,
                        CategoryId = req.CategoryId,
                        ItemName = req.ItemName,
                        ItemAmount = req.ItemAmount,
                        InoutFlg = req.InoutFlg,
                        UsedDate = usedDate,
                        FrequencyId = frequency.Id,
                    };

                    // リストに追加
                    items.Add(item);

                    // 日付を指定の月数進める
                    usedDate = usedDate.AddMonths(frequencyNumber);
                }
            }
            else if(req.Frequency == (int)Frequency.EveryYear)
            {
                // 固定費管理テーブル登録用データの作成
                KakeiboItemFrequency frequency = new()
                {
                    KakeiboId = req.KakeiboId,
                    CategoryId = req.CategoryId,
                    ItemName = req.ItemName,
                    ItemAmount = req.ItemAmount,
                    InoutFlg = req.InoutFlg,
                    Frequency = req.Frequency,
                    FixedStartDate = req.UsedDate,
                    FixedEndDate = req.FixedEndDate,
                };

                // 固定費管理テーブルに登録を実施
                await this.kakeiboRepositoy.RegistKakeiboItemFrequencyAsync(frequency);

                // 繰り返し終了日付に到達するまで繰り返し
                while (req.FixedEndDate > usedDate)
                {
                    KakeiboItem item = new KakeiboItem()
                    {
                        KakeiboId = req.KakeiboId,
                        CategoryId = req.CategoryId,
                        ItemName = req.ItemName,
                        ItemAmount = req.ItemAmount,
                        InoutFlg = req.InoutFlg,
                        UsedDate = usedDate,
                        FrequencyId = frequency.Id,
                    };

                    // リストに追加
                    items.Add(item);

                    // 日付を指定の年数進める
                    usedDate = usedDate.AddYears(frequencyNumber);
                }
            }

            //配列でまとめて登録
            return new RegistKakeiboItemResponse()
            {
                Count = await this.kakeiboRepositoy.RegistKakeiboItemListAsync(items)
            };
        }

        /// <summary>
        /// アイテム更新
        /// <summary>
        public async Task<UpdateKakeiboItemResponse> UpdateKakeiboItemAsync(UpdateKakeiboItemRequest req)
        {
            // 該当アイテム取得
            KakeiboItem? item = await this.kakeiboRepositoy.GetKakeiboItemAsync(req.ItemId);

            if(item == null)
            {
                throw new Exception("アイテムが存在しません");
            }

            // 該当固定費管理データ取得
            KakeiboItemFrequency? frequency = await this.kakeiboRepositoy.GetKakeiboItemFrequencyAsync(item.FrequencyId);

            if(frequency == null)
            {
                throw new Exception("固定費管理データが存在しません");
            }
            else if(frequency.Frequency == (int)Frequency.OnlyOnce)
            {
                // 単品登録の更新の場合はアイテムの更新のみでOK。
                item.CategoryId = req.CategoryId;
                item.ItemName = req.ItemName;
                item.ItemAmount = req.ItemAmount;
                item.InoutFlg = req.InoutFlg;
                item.UsedDate = req.UsedDate;

                return new UpdateKakeiboItemResponse()
                {
                    Count = await this.kakeiboRepositoy.UpdateKakeiboItemAsync(item)
                };
            }
            else
            {
                // 単品登録以外の場合、それ以降の全部の変更orそれ単体の変更
                if (req.ChangeFlg)
                {
                    // 指定アイテムの利用日
                    DateTime startTime = item.UsedDate;

                    // 現状存在する固定費管理データの終了日時(新しい固定費管理データの終了日時となる)
                    DateTime endTime = frequency.FixedEndDate!.Value;

                    // 現状存在する固定費管理データの終了日時を指定アイテムの利用日-1日で登録
                    frequency.FixedEndDate = startTime.AddDays(-1);
                    frequency.UpdateDate = DateTime.Now;

                    // 旧固定費管理データの更新
                    await this.kakeiboRepositoy.UpdateKakeiboItemFrequencyAsync(frequency);

                    // 続いて、新規固定費管理データを登録
                    KakeiboItemFrequency newFrequency = new()
                    {
                        KakeiboId = frequency.KakeiboId,
                        CategoryId = req.CategoryId,
                        ItemName = req.ItemName,
                        ItemAmount = req.ItemAmount,
                        InoutFlg = req.InoutFlg,
                        Frequency = frequency.Frequency,
                        FixedStartDate = startTime,
                        FixedEndDate = endTime,
                    };

                    await this.kakeiboRepositoy.RegistKakeiboItemFrequencyAsync(newFrequency);

                    // 続いて、該当全リストを取得して中身の更新を行う
                    List<KakeiboItem>? items = await this.kakeiboRepositoy.GetKakeiboItemListAsync(new()
                    {
                        frequencyId = frequency.Id,
                        StartDate = startTime,
                    });

                    // 取得したアイテムの中身を書き換えてまとめて更新する
                    foreach (var kakeiboItem in items)
                    {
                        // 頻度・利用日を変える必要はない。
                        kakeiboItem.FrequencyId = newFrequency.Id;
                        kakeiboItem.CategoryId = req.CategoryId;
                        kakeiboItem.ItemName = req.ItemName;
                        kakeiboItem.ItemAmount = req.ItemAmount;
                        kakeiboItem.InoutFlg = req.InoutFlg;
                    }

                    return new UpdateKakeiboItemResponse()
                    {
                        Count = await this.kakeiboRepositoy.UpdateKakeiboItemListAsync(items)
                    };
                }
                else
                {
                    // 該当アイテムのみを更新する場合、新規固定費管理データを登録とアイテムの更新のみ
                    KakeiboItemFrequency newFrequency = new()
                    {
                        KakeiboId = frequency.KakeiboId,
                        Frequency = (int)Frequency.OnlyOnce,
                        CategoryId = frequency.CategoryId
                    };

                    await this.kakeiboRepositoy.RegistKakeiboItemFrequencyAsync(newFrequency);

                    // 単品登録の更新の場合はアイテムの更新のみでOK。
                    item.CategoryId = req.CategoryId;
                    item.ItemName = req.ItemName;
                    item.ItemAmount = req.ItemAmount;
                    item.InoutFlg = req.InoutFlg;
                    item.UsedDate = req.UsedDate;
                    item.FrequencyId = newFrequency.Id;

                    return new UpdateKakeiboItemResponse()
                    {
                        Count = await this.kakeiboRepositoy.UpdateKakeiboItemAsync(item)
                    };
                }
            }
        }

        /// <summary>
        /// アイテム論理削除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DeleteKakeiboItemResponse> DeleteKakeiboItemAsync(DeleteKakeiboItemRequest req)
        {
            KakeiboItem? item = await this.kakeiboRepositoy.GetKakeiboItemAsync(req.Id);

            if (item == null)
            {
                throw new Exception("アイテムがありません");
            }
            else
            {
                // 論理削除
                item.DeleteDate = DateTime.Now;

                DeleteKakeiboItemResponse response = new()
                {
                    Count = await this.kakeiboRepositoy.UpdateKakeiboItemAsync(item)
                };
                return response;
            }
        }
    }
}
