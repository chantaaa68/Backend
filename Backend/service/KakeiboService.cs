using Backend.Annotation;
using Backend.Dto.common;
using Backend.Dto.repository;
using Backend.Dto.service;
using Backend.Dto.service.kakeibo;
using Backend.Model;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using WebApplication.Context;

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
            KakeiboItem item = new KakeiboItem()
            {
                CategoryId = req.CategoryId,
                ItemName = req.ItemName,
                ItemAmount = req.ItemAmount,
                InoutFlg = req.InoutFlg,
                UsedDate = req.UsedDate,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            RegistKakeiboItemResponse response = new()
            {
                Count = await this.kakeiboRepositoy.RegistKakeiboItemAsync(item)
            };

            return response;
        }

        /// <summary>
        /// アイテム更新
        /// <summary>
        public async Task<UpdateKakeiboItemResponse> UpdateKakeiboItemAsync(UpdateKakeiboItemRequest req)
        {
            // フロント側でリクエストする時点で存在しないことなないので。
            KakeiboItem? item = await this.kakeiboRepositoy.GetKakeiboItemAsync(req.Id);

            if(item== null)
            {
                throw new Exception("アイテムがありません");
            }
            else
            {
                item.CategoryId = req.CategoryId ?? item.CategoryId;
                item.ItemName = req.ItemName ?? item.ItemName;
                item.ItemAmount = req.ItemAmount ?? item.ItemAmount;
                item.InoutFlg = req.InoutFlg ?? item.InoutFlg;
                item.UsedDate = req.UsedDate ?? item.UsedDate;
                item.UpdateDate = DateTime.Now;

                UpdateKakeiboItemResponse response = new()
                {
                    Count = await this.kakeiboRepositoy.UpdateKakeiboItemAsync(item)
                };
                return response;
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
