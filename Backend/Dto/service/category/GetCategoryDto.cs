using Backend.Dto.common;

namespace Backend.Dto.service.category
{
    public class GetCategoryDataRequest
    {
        public int UserId { get; set; }

        /**
         * デフォルトカテゴリを取得して返却するか否かのフラグ
         */
        public bool DefaultFlg { get; set; }
    }

    public class GetCategoryDataResponse
    {
        public List<CategoryItem>? Categories { get; set; }
    }
}
