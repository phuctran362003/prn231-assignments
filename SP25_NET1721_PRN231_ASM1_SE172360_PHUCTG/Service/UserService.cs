using Repository;
using Repository.Models;

namespace Service;

public interface IUserService
{
    Task<User> Authenticate(string email, string password);
}

public class UserService : IUserService
{
    public UserRepository _repository;

    public UserService()
    {
        _repository ??= new UserRepository();
    }

    public async Task<User> Authenticate(string email, string password)
    {
        return await _repository.GetUserAccount(email, password);
    }
}