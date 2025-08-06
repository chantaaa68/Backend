using WebApplication.Dto;
using WebApplication.Model;
using WebApplication.Repository;

namespace WebApplication.service
{
    public class UserDataService
    {
        private readonly UserDataRepository repository;

        public UserDataService(UserDataRepository repository)
        {
            this.repository = repository;
        }

        public UserResponse GetAllUserData()
        {
            List<Dto.User> users = new List<Dto.User>();

            List<Model.User> userList = this.repository.SelectAllData();

            userList.ForEach(user =>
            {
                var res = new Dto.User
                {
                    Id = user.Id,
                    UserName = user.Name,
                    Email = user.Email
                };
                users.Add(res);
            });

            UserResponse response = new UserResponse
            {
                Users = users
            };

            return response;
        }

        public RegistResponse RegistUser(RegistRequest request)
        {
            Model.User data = new()
            {
                Name = request.UserName,
                Email = request.Email,
                UserHash = Guid.NewGuid().ToString(), // 仮のハッシュ生成
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            this.repository.UserRigist(data);

            RegistResponse response = new RegistResponse
            {
                Id = this.repository.GetUserId(request.Email)
            };

            return response;
        }
    }
}
