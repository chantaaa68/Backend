namespace WebApplication.Dto
{
    public class UserRequest
    {
        public required string UserName { get; set; }

        public required string Email { get; set; }

    }

    public class UserResponse
    {
        public required List<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }

    public class RegistRequest
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }

    public class RegistResponse
    {
        public int Id { get; set; }
    }
}
