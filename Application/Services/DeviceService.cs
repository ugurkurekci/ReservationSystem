namespace Application.Services;

public class DeviceService
{
    public Task<bool> IsDeviceAvailableAsync(int deviceId)
    {
        return Task.FromResult(true);
    }
}