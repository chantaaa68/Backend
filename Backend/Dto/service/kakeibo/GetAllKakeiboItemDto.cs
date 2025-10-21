using Backend.Dto.common;
using Backend.Dto.repository;
using System.ComponentModel.DataAnnotations;

namespace Backend.Dto.service.kakeibo
{
    public class GetMonthlyResultRequest
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        [Required]
        public int UserId { get; set; }
    }

    public class GetMonthlyResultResponse
    {
        /// <summary>
        /// 各カテゴリごとの支出合計データ
        /// </summary>
        public required List<MonthlyReportItem> MonthlyExpenses { get; set; }

        /// <summary>
        /// 各カテゴリごとの収入合計データ
        /// </summary>
        public required List<MonthlyReportItem> MonthlyIncomes { get; set; }
    }
}
