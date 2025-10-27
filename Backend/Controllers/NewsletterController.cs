using Backend.Annotation;
using Backend.Dto.service.newsletter;
using Backend.service;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Component]
    [Route("api/[controller]/[action]")]
    public class NewsletterController(NewsletterService _newsletterService) : ControllerBase
    {
        private readonly NewsletterService newsletterService = _newsletterService;

        /// <summary>
        /// テンプレートを登録する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RigistNewsletterAsync([FromBody] RigistNewsletterRequest req)
        {
           return await this.newsletterService.ResistNewsletterAsync(req);
        }

        /// <summary>
        /// テンプレートを更新する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateNewsletterAsync([FromBody] UpdateNewsletterRequest req)
        {
            return await this.newsletterService.UpdateNewsletterAsync(req);
        }

        /// <summary>
        /// メール送信
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendMailAsync([FromBody] SendNewsletterRequest request)
        {
            return await this.newsletterService.SendNewsletterAsync(request);

        }
    }
}
