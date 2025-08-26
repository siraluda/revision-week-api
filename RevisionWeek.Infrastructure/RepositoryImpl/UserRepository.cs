using Domain.AppUser;

namespace RevisionWeek.Infrastructure.RepositoryImpl;

public class UserRepository : IUserRepository
{
    public Task<User> GetUserByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}