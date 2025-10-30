namespace Backend.Dto.common
{
    public class CategoryItem
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = null!;

        public Boolean InoutFlg { get; set; }

        public string IconName { get; set; } = null!;
    }
}
