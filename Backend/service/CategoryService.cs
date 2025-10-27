using Backend.Annotation;
using Backend.Dto.common;
using Backend.Dto.service.category;
using Backend.Model;
using Backend.Repository;
using Backend.Utility;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetCategoryDataAsync(GetCategoryDataRequest req)
        {
            //ユーザー登録カテゴリの取得
            int? kakeiboId = await this.kakeiboRepository.GetKakeiboIdAsync(req.UserId);

            if (kakeiboId == null)
            {
                return ApiResponseHelper.Fail("家計簿が存在しません。");
            }

            // ユーザーIDを基に家計簿IDを取得してセット
            List<CategoryItem> userCategoryItems = await this.categoryRepository.GetCategoryAsync(kakeiboId.Value);

            //デフォルトカテゴリの取得
            List<CategoryItem> categoryItems = await this.categoryRepository.GetCategoryDefaultAsync();

            if (userCategoryItems.Count == 0)
            {
                //ユーザー登録カテゴリが存在しない場合、デフォルトカテゴリを返す
                GetCategoryDataResponse response = new()
                {
                    Categories = categoryItems
                };

                return ApiResponseHelper.Success(response);
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

                GetCategoryDataResponse response = new()
                {
                    Categories = mergedCategories.Values.ToList()
                };

                return ApiResponseHelper.Success(response);
            }
        }

        /// <summary>
        /// ユーザカテゴリを登録する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<IActionResult> RegistCategoryAsync(RegistCategoryRequest req)
        {
            int? kakeiboId = await this.kakeiboRepository.GetKakeiboIdAsync(req.UserId);
            if (kakeiboId == null)
            {
                return ApiResponseHelper.Fail("家計簿が存在しません。");
            }

            int? IconId = await this.iconRepository.GetIconIdAsync(req.IconName);

            if (IconId == null) {
                return ApiResponseHelper.Fail("指定されたアイコンが存在しません。");
            }

            Category category = new()
            {
                KakeiboID = kakeiboId.Value,
                CategoryName = req.CategoryName,
                InoutFlg = req.InoutFlg,
                IconId = IconId.Value
            };

            RegistCategoryResponse response = new()
            {
                CategoryId = await this.categoryRepository.RegistCategoryAsync(category)
            };

            return ApiResponseHelper.Success(response);
        }

        /// <summary>
        /// ユーザーカテゴリの更新を行う
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> UpdateCategoryAsync(UpdateCategoryRequest req)
        {
            Category? category = await this.categoryRepository.GetCategoryByIdAsync(req.Id);

            if(category == null)
            {
                return ApiResponseHelper.Fail("指定されたカテゴリが存在しません");
            }
            else
            {
                category.CategoryName = req.CategoryName ?? category.CategoryName;
                category.InoutFlg = req.InoutFlg ?? category.InoutFlg;
                category.IconId = req.IconName != null ? (int)await this.iconRepository.GetIconIdAsync(req.IconName) : category.IconId;

                UpdateCategoryResponse response = new()
                {
                    CategoryId = await this.categoryRepository.UpdateCategoryAsync(category)
                };

                return ApiResponseHelper.Success(response);
            }
        }
    }
}
