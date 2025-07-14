using WebApplication.Context;
using WebApplication.Dto;
using WebApplication.Model;

namespace WebApplication.Repository
{
    public class UserDataRepository
    {
        private readonly AWSDbContext _dbContext;

        public UserDataRepository(AWSDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<UserData> SelectAllData()
        {
            var userData = _dbContext.UserDatas.ToList();

            return userData;
        }

        public UserData UserRigist(UserData request)
        {
            this._dbContext.UserDatas.Add(request);

            this._dbContext.SaveChanges();

            return request;
        }

        public int GetUserId(string email)
        {
            var data = this._dbContext.UserDatas.Where(e => e.Email == email).ToList();

            return data[0].Id;
        }

        public UserData GetUserById(int id)
        {
            return this._dbContext.UserDatas.FirstOrDefault(u => u.Id == id)
                   ?? throw new KeyNotFoundException($"User with ID {id} not found.");
        }
    }
}
