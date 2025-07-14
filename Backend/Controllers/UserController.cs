using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;
using WebApplication.service;
using static Backend.Dto.NewsletterDto;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserDataService service;

        public UserController(UserDataService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(this.service.GetAllUserData());
        }

        [HttpPost]
        public IActionResult Regist(RegistRequest request)
        {
            return Ok(this.service.RegistUser(request));
        }
    }
}
