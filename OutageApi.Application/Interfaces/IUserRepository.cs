public interface IUserRepository
{
    Task<bool> AuthenticateUserAsync(string username, string password);
    Task<List<User>> GetAllUsersAsync();
}