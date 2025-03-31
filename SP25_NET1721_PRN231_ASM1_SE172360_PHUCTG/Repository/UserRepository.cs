using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Models;

namespace Repository;

public class UserRepository : GenericRepository<User>
{
    public UserRepository() { }
    public async Task<User> GetUserAccount(string email, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
    }
}