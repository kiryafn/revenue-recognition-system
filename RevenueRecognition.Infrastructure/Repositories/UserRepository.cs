using Microsoft.EntityFrameworkCore;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : IUserRepository
{
    public async Task<User?> GetByLoginAsync(string login, CancellationToken ct = default) =>
        await db.Users
            .FirstOrDefaultAsync(e => e.Login == login, ct);

    public async Task<User> AddAsync(User user, CancellationToken ct = default)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync(ct);
        return user;
    }
}