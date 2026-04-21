using Microsoft.EntityFrameworkCore;
using OutageApi.Infrastructure.Data;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AuthenticateUserAsync(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
        if (user == null) return false;

        // In a real application, you should hash and salt passwords
        return user.Password == password;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}