using Application.Commands;
using Application.Services;
using Application.Validation.Abstracts;
using Application.Validation.Dto;

namespace Application.Validation;

public class BalanceValidationRule : IValidationRule
{

    private readonly BalanceService _balanceService;

    public BalanceValidationRule(BalanceService balanceService)
    {
        _balanceService = balanceService;
    }

    public async Task<ProjectResult> ValidateAsync(CreateReservationCommand command)
    {

        bool balance = await _balanceService.HasBalanceSufficient(command.UserId);
        if (!balance)
        {
            return new ProjectResult { IsValid = false, Message = "Yetersiz bakiye." };
        }

        return new ProjectResult { IsValid = true };

    }
}
