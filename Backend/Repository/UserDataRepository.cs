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

        public List<Model.User> SelectAllData()
        {
            var userData = _dbContext.Users.ToList();

            return userData;
        }

        public Model.User UserRigist(Model.User request)
        {
            this._dbContext.Users.Add(request);

            this._dbContext.SaveChanges();

            return request;
        }

        public int GetUserId(string email)
        {
            var data = this._dbContext.Users.Where(e => e.Email == email).ToList();

            return data[0].Id;
        }

        public Model.User GetUserById(int id)
        {
            return this._dbContext.Users.FirstOrDefault(u => u.Id == id)
                   ?? throw new KeyNotFoundException($"User with ID {id} not found.");
        }
    }
}
