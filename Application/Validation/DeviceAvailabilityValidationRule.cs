using Application.Commands;
using Application.Services;
using Application.Validation.Abstracts;
using Application.Validation.Dto;

namespace Application.Validation;

public class DeviceAvailabilityValidationRule : IValidationRule
{
    private readonly DeviceService _deviceService;

    public DeviceAvailabilityValidationRule(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    public async Task<ProjectResult> ValidateAsync(CreateReservationCommand command)
    {
        var isDeviceAvailable = await _deviceService.IsDeviceAvailableAsync(command.DeviceId);
        if (!isDeviceAvailable)
        {
            return new ProjectResult { IsValid = false, Message = "Cihaz kullanılabilir değil." };
        }

        return new ProjectResult { IsValid = true };
    }
}