using Backend.service;
using Microsoft.AspNetCore.Mvc;
using static Backend.Dto.NewsletterDto;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NewsletterController: ControllerBase
    {
        private readonly NewsletterService newsletterService;
        public NewsletterController(NewsletterService newsletterService)
        {
            this.newsletterService = newsletterService;
        }
        [HttpPost]
        public IActionResult Register(NewsletterRigistRequest request)
        {
            this.newsletterService.NewsletterResist(request);
            return Ok();
        }
        [HttpPost]
        public IActionResult SendMail(NewsletterSendRequest request)
        {
            var response = this.newsletterService.NewsletterSend(request);
            return Ok(response);
        }
    }
   
}
