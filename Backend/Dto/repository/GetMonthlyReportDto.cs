using Backend.Dto.common;

namespace Backend.Dto.repository
{
    public class GetMonthlyReportResult
    {
        /// <summary>
        /// 収支データ
        /// </summary>
        public required List<MonthlyReport> MonthlyReports { get; set; }
    }

    public class MonthlyReport
    {
        /// <summary>
        /// 収支フラグ（true:収入、false:支出）
        /// </summary>
        public required bool InoutFlg { get; set; }

        /// <summary>
        /// 収支フラグ別データ
        /// </summary>
        public required List<MonthlyReportItem> MonthlyReportItems { get; set; }
    }
}
