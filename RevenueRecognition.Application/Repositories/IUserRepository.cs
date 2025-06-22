using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetByLoginAsync(string login, CancellationToken ct = default);
    Task<User>  AddAsync(User user, CancellationToken ct = default);

}