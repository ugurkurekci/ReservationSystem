using Application.Commands;
using Application.Services;
using Application.Validation.Abstracts;
using Application.Validation.Dto;

namespace Application.Validation;

public class UserExistsValidationRule : IValidationRule
{
    private readonly UserService _userService;

    public UserExistsValidationRule(UserService userService)
    {
        _userService = userService;
    }

    public async Task<ProjectResult> ValidateAsync(CreateReservationCommand command)
    {
        bool user = await _userService.UserExistsAsync(command.UserId);
        if (!user)
        {
            return new ProjectResult { IsValid = false, Message = "Kullanıcı bulunamadı." };
        }

        return new ProjectResult { IsValid = true };
    }
}