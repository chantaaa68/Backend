using Backend.Annotation;
using Backend.Dto.service.user;
using Backend.Model;
using Backend.Repository;
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
        public async Task<GetUserDataResponse> GetUserDataAsync(GetUserDataRequest req)
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

            return response;
        }

        /// <summary>
        /// ユーザー新規登録・家計簿データの作成
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<RegistUserResponse> RegistAsync(RegistUserRequest req)
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
            return new RegistUserResponse(){ UserId = userId };
        }

        /// <summary>
        /// ユーザーデータと家計簿データを更新する
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<UpdateUserResponse> UpdateAsync(UpdateUserRequest req)
        {
            // 該当ユーザー取得
            Users? user = await this.userDataRepository.GetUserAsync(req.UserId);

            if (!string.IsNullOrEmpty(req.UserName) || !string.IsNullOrEmpty(req.Email))
            {
                //TODO: エラーハンドリングは400で
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {req.UserId} not found.");
                }

                // ユーザー情報更新
                user.Name = req.UserName ?? user.Name;
                user.Email = req.Email ?? user.Email;
            }

            if(!string.IsNullOrEmpty(req.KakeiboName) || !string.IsNullOrEmpty(req.KakeiboExplanation))
            {
                if (user.Kakeibo == null)
                {
                    throw new Exception("家計簿が存在しません");
                }

                // 家計簿更新
                user.Kakeibo.KakeiboName = req.KakeiboName ?? user.Kakeibo.KakeiboName;
                user.Kakeibo.KakeiboExplanation = req.KakeiboExplanation ?? user.Kakeibo.KakeiboExplanation;
            }

            // 情報更新
            int userId = await this.userDataRepository.UpdateUserAsync(user);

            return new UpdateUserResponse() { UserId = userId };
        }

        /// <summary>
        /// ユーザーデータの削除＆家計簿データの削除
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DeleteUserResponse> DeleteAsync(DeleteUserRequest req)
        {
            Users? user = await this.userDataRepository.GetUserAsync(req.UserId);

            if (user == null)
            {
                throw new Exception("ユーザーが存在しません");
            }

            user.DeleteDate = DateTime.Now;
            user.Kakeibo.DeleteDate = DateTime.Now;

            int userId = await this.userDataRepository.UpdateUserAsync(user);

            return new DeleteUserResponse() { UserId = userId };
        }
    }
}
