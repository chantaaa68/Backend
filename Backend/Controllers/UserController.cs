using Backend.Annotation;
using Backend.Dto.service.user;
using Microsoft.AspNetCore.Mvc;
using WebApplication.service;

namespace Backend.Controllers
{
    [Component]
    [Route("api/[controller]/[action]")]
    public class UserController(UserDataService _userDataService) : ControllerBase
    {
        private readonly UserDataService userDataService = _userDataService;

        /// <summary>
        /// ユーザー・家計簿データの取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<GetUserDataResponse> GetUserDataAsync([FromQuery] GetUserDataRequest req)
        {
            return await this.userDataService.GetUserDataAsync(req);
        }

        /// <summary>
        /// ユーザーと家計簿情報を登録する
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RegistUserResponse> RegistAsync([FromBody] RegistUserRequest req)
        {
            return await this.userDataService.RegistAsync(req);
        }

        /// <summary>
        /// ユーザー・家計簿情報を更新する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UpdateUserResponse> UpdateAsync([FromBody] UpdateUserRequest req)
        {
            return await this.userDataService.UpdateAsync(req);
        }

        /// <summary>
        /// ユーザー・家計簿データを削除する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DeleteUserResponse> DeleteAsync([FromBody] DeleteUserRequest req)
        {
            return await this.userDataService.DeleteAsync(req);
        }
    }
}
