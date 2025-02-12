namespace Application.Services;

public class UserService
{

    public Task<bool> UserExistsAsync(int userId)
    {
        return Task.FromResult(true);
    }

}