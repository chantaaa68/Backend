using Backend.Annotation;
using Backend.Dto.service.icon;
using Backend.service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Component]
    [Route("api/[controller]/[action]")]
    public class IconController(IconService _service)
    {
        public readonly IconService service = _service;

        /// <summary>
        /// アイコンのリストを取得する
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetIconListResponse> GetIconListAsync()
        {
            return await service.GetIconListAsync();
        }
    }
}
