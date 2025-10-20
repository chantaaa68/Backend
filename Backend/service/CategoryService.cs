using Backend.Annotation;
using Backend.Dto.common;
using Backend.Dto.service.category;
using Backend.Model;
using Backend.Repository;

namespace Backend.service
{
    [Component]
    public class CategoryService(
        CategoryRepositoy _categoryRepository,
        KakeiboRepository _kakeiboRepository,
        IconRepository _iconRepository
        )
    {
        public readonly CategoryRepositoy categoryRepository = _categoryRepository;

        public readonly KakeiboRepository kakeiboRepository = _kakeiboRepository;

        public readonly IconRepository iconRepository = _iconRepository;

        /// <summary>
        /// ユーザー登録カテゴリデータを取得する。存在しない場合、デフォルトカテゴリを返却する。
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<GetCategoryDataResponse> GetCategoryDataAsync(GetCategoryDataRequest req)
        {
            //ユーザー登録カテゴリの取得
            // ユーザーIDを基に家計簿IDを取得してセット
            List<CategoryItem> userCategoryItems = await this.categoryRepository.GetCategoryAsync(await this.kakeiboRepository.GetKakeiboIdAsync(req.UserId));

            //デフォルトカテゴリの取得
            List<CategoryItem> categoryItems = await this.categoryRepository.GetCategoryDefaultAsync();

            if (userCategoryItems.Count == 0)
            {
                //ユーザー登録カテゴリが存在しない場合、デフォルトカテゴリを返す
                return new GetCategoryDataResponse
                {
                    Categories = categoryItems
                };
            }
            else
            {
                // アイコン名で合致するデータを上書き
                Dictionary<string,CategoryItem> mergedCategories = categoryItems.ToDictionary(c => c.IconName);

                //アイコン名は一意なので、アイコン名で合致するデータを上書きする
                foreach (CategoryItem categoryItem in userCategoryItems)
                {
                    mergedCategories[categoryItem.IconName] = categoryItem;
                }

                return new GetCategoryDataResponse
                {
                    Categories = mergedCategories.Values.ToList()
                };
            }
        }

        /// <summary>
        /// ユーザカテゴリを登録する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<RegistCategoryResponse> RegistCategoryAsync(RegistCategoryRequest req)
        {
            Category category = new()
            {
                KakeiboID = await this.kakeiboRepository.GetKakeiboIdAsync(req.UserId),
                CategoryName = req.CategoryName,
                InoutFlg = req.InoutFlg,
                IconId = await this.iconRepository.GetIconIdAsync(req.IconName),
            };

            RegistCategoryResponse response = new()
            {
                CategoryId = await this.categoryRepository.RegistCategoryAsync(category)
            };

            return response;
        }

        /// <summary>
        /// ユーザーカテゴリの更新を行う
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UpdateCategoryResponse> UpdateCategoryAsync(UpdateCategoryRequest req)
        {
            Category? category = await this.categoryRepository.GetCategoryByIdAsync(req.Id);

            if(category == null)
            {
                throw new Exception("指定されたカテゴリが存在しません。");
            }
            else
            {
                category.CategoryName = req.CategoryName ?? category.CategoryName;
                category.InoutFlg = req.InoutFlg ?? category.InoutFlg;
                category.IconId = req.IconName != null ? await this.iconRepository.GetIconIdAsync(req.IconName) : category.IconId;

                UpdateCategoryResponse response = new()
                {
                    CategoryId = await this.categoryRepository.UpdateCategoryAsync(category)
                };

                return response;
            }
        }
    }
}
