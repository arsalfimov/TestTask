using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Exceptions;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services;
public class UserService : IUserService
{
    private readonly ApplicationDbContext _dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUser() =>
        await _dbContext.Users
            .OrderByDescending(u => u.Orders.Count)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException(nameof(Order));

    public async Task<List<User>> GetUsers() =>
        await _dbContext.Users
            .Where(u => u.Status == UserStatus.Inactive)
            .ToListAsync();
}
