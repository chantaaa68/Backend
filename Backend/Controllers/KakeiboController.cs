using Backend.Annotation;
using Backend.Dto.service;
using Backend.Dto.service.kakeibo;
using Backend.service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [AutoDI]
    [ApiController]
    [Route("api/[controller]/[action]")]
    
    public class KakeiboController(KakeiboService service) : ControllerBase
    {
        public KakeiboService _service = service;

        /// <summary>
        /// 家計簿の更新
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateKakeiboAsync([FromBody] UpdateKakeiboRequest req)
        {
            return await this._service.UpdateKakeiboAsync(req);
        }

        /// <summary>
        /// 月間レポートを取得する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMonthlyResultAsync([FromQuery] GetMonthlyResultRequest req)
        {
            return await this._service.GetMonthlyResultAsync(req);
        }

        /// <summary>
        /// アイテム詳細データを取得する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetKakeiboItemDetailAsync([FromQuery] GetKakeiboItemDetailRequest req)
        {
            return await this._service.GetKakeiboItemDetailAsync(req);
        }

        /// <summary>
        /// アイテムを登録する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegistKakeiboItemAsync([FromBody] RegistKakeiboItemRequest req)
        {
            return await this._service.RegistKakeiboItemAsync(req);
        }

        /// <summary>
        /// アイテムを更新する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateKakeiboItemAsync([FromBody] UpdateKakeiboItemRequest req)
        {
            return await this._service.UpdateKakeiboItemAsync(req);
        }

        /// <summary>
        /// アイテムを削除する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteKakeiboItemAsync([FromBody] DeleteKakeiboItemRequest req)
        {
            return await this._service.DeleteKakeiboItemAsync(req);
        }
    }
}
