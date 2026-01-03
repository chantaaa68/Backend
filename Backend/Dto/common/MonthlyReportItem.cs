namespace Backend.Dto.common
{
    public class MonthlyReportItem
    {
        /// <summary>
        /// 集計月(yyyy-mm)
        /// </summary>
        public required string UsedMonth { get; set; }

        /// <summary>
        /// 集計月別データ
        /// </summary>
        public required List<CategoryReportItem> CategoryReportItems { get; set; }
    }

    public class CategoryReportItem
    {
        /// <summary>
        /// カテゴリ名
        /// </summary>
        public required string CategoryName { get; set; }

        /// <summary>
        /// アイコン名
        /// </summary>
        public required string IconName { get; set; }

        /// <summary>
        /// カテゴリごとの合計金額
        /// </summary>
        public required int TotalAmount { get; set; }
    }
}
