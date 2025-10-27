using Backend.Annotation;
using Backend.Dto.service.category;
using Backend.service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    [Component]
    [Route("api/[controller]/[action]")]
    public class CategoryController(CategoryService _categoryService)
    {
        public readonly CategoryService categoryService = _categoryService;

        /// <summary>
        /// ユーザー登録カテゴリを登録する。存在しない場合、デフォルトカテゴリを返却する。
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCategoryDataAsync([FromQuery] GetCategoryDataRequest req)
        {
            return await this.categoryService.GetCategoryDataAsync(req);
        }

        /// <summary>
        /// ユーザーカテゴリを登録する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegistCategoryAsync([FromBody] RegistCategoryRequest req)
        {
            return await this.categoryService.RegistCategoryAsync(req);
        }


        /// <summary>
        /// ユーザーカテゴリの更新を行う
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryRequest req)
        {
            return await this.categoryService.UpdateCategoryAsync(req);
        }
    }
}
