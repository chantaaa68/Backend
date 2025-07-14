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
            List<User> users = new List<User>();

            List<UserData> userList = this.repository.SelectAllData();

            userList.ForEach(user =>
            {
                var res = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
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
            UserData data = new UserData
            {
                UserName = request.UserName,
                Email = request.Email
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
