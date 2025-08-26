namespace Domain.AppUser;

public interface IUserRepository
{
    public Task<User> GetUserByIdAsync(string id);
}