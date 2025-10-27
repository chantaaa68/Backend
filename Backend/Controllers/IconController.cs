using Backend.Annotation;
using Backend.Dto.service.icon;
using Backend.service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [AutoDI]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IconController(IconService _service) : ControllerBase
    {
        public readonly IconService service = _service;

        /// <summary>
        /// アイコンのリストを取得する
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetIconListAsync()
        {
            return await service.GetIconListAsync();
        }
    }
}
