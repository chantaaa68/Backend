namespace Backend.Dto.service.icon
{
    public class GetIconListResponse
    {
        public required List<IconData> IconDatas { get; set; }

    }

    public class IconData
    {
        public required int IconId { get; set; }

        public required string OfficialIconName { get; set; }

        public required string DefaultIconName { get; set; }
    }
}
