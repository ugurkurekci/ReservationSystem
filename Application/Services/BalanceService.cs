namespace Application.Services;
public class BalanceService
{
    public Task<bool> HasBalanceSufficient(int userId)
    {
        return Task.FromResult(true);
    }
}
