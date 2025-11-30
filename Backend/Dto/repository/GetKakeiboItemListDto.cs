namespace Backend.Dto.repository
{
    public class GetKakeiboItemListParameter
    {
        public int KakeiboId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetKakeiboItemListResult
    {
        public required List<KakeiboItemInfo> KakeiboItemInfos { get; set; }
    }

    public class KakeiboItemInfo
    {

        public required int DayNo { get; set; }

        public required List<Item> Items { get; set; }
    }

    public class Item
    {
        public required int ItemId { get; set; }

        public string? ItemName { get; set; }

        public required int ItemAmount { get; set; }

        public required bool InoutFlg { get; set; }

        public required DateTime UsedDate { get; set; }

        public required string IconName { get; set; }
    }
}
