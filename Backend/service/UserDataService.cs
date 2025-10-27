using Backend.Annotation;
using Backend.Dto.service.user;
using Backend.Model;
using Backend.Repository;
using Backend.Utility;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Model;
using WebApplication.Repository;

namespace WebApplication.service
{
    [Component]
    public class UserDataService(
        UserDataRepository _userDataRepository,
        KakeiboRepository _kakeiboRepository
        )
    {
        private readonly UserDataRepository userDataRepository = _userDataRepository;

        private readonly KakeiboRepository kakeiboRepository = _kakeiboRepository;


        /// <summary>
        /// ユーザー・家計簿データの取得
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetUserDataAsync(GetUserDataRequest req)
        {
            // 該当ユーザー取得
            Users? user = await this.userDataRepository.GetUserAsync(req.Id);

            // レスポンス返却
            GetUserDataResponse response = new()
            {
                UserName = user.Name,
                Email = user.Email,
                KakeiboName = user.Kakeibo.KakeiboName,
                KakeiboExplanation = user.Kakeibo.KakeiboExplanation
            };

            return ApiResponseHelper.Success(response);
        }

        /// <summary>
        /// ユーザー新規登録・家計簿データの作成
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<IActionResult> RegistAsync(RegistUserRequest req)
        {
            // ユーザーデータの作成
            Users data = new()
            {
                Name = req.UserName,
                Email = req.Email,
                UserHash = Guid.NewGuid().ToString(), // 仮のハッシュ生成
            };

            // TODO: トランザクションの検討
            int userId = await this.userDataRepository.UserRigistAsync(data);

            // 家計簿データの作成
            Kakeibo kakeibo = new()
            {
                UserId = userId,
                KakeiboName = req.KakeiboName,
                KakeiboExplanation = req.KakeiboExplanation,
            };

            await this.kakeiboRepository.RegistKakeiboAsync(kakeibo);

            // レスポンス返却
            RegistUserResponse response =  new()
            { 
                UserId = userId 
            };

            return ApiResponseHelper.Success(response);
        }

        /// <summary>
        /// ユーザーデータと家計簿データを更新する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> UpdateAsync(UpdateUserRequest req)
        {
            // 該当ユーザー取得
            Users? user = await this.userDataRepository.GetUserAsync(req.UserId);

            if (!string.IsNullOrEmpty(req.UserName) || !string.IsNullOrEmpty(req.Email))
            {
                if (user == null)
                {
                    return ApiResponseHelper.Fail("ユーザーが存在しません");
                }

                // ユーザー情報更新
                user.Name = req.UserName ?? user.Name;
                user.Email = req.Email ?? user.Email;
            }

            if(!string.IsNullOrEmpty(req.KakeiboName) || !string.IsNullOrEmpty(req.KakeiboExplanation))
            {
                if (user.Kakeibo == null)
                {
                    return ApiResponseHelper.Fail("家計簿が存在しません");
                }

                // 家計簿更新
                user.Kakeibo.KakeiboName = req.KakeiboName ?? user.Kakeibo.KakeiboName;
                user.Kakeibo.KakeiboExplanation = req.KakeiboExplanation ?? user.Kakeibo.KakeiboExplanation;
            }

            // 情報更新
            int userId = await this.userDataRepository.UpdateUserAsync(user);

            UpdateUserResponse response = new() 
            { 
                UserId = userId 
            };

            return ApiResponseHelper.Success(response);
        }

        /// <summary>
        /// ユーザーデータの削除＆家計簿データの削除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IActionResult> DeleteAsync(DeleteUserRequest req)
        {
            Users? user = await this.userDataRepository.GetUserAsync(req.UserId);

            if (user == null)
            {
                return ApiResponseHelper.Fail("ユーザーが存在しません");
            }

            user.DeleteDate = DateTime.Now;
            user.Kakeibo.DeleteDate = DateTime.Now;

            int userId = await this.userDataRepository.UpdateUserAsync(user);

            DeleteUserResponse response =  new() 
            { 
                UserId = userId 
            };

            return ApiResponseHelper.Success(response);
        }
    }
}
