using Application.Commands;
using Application.Validation.Abstracts;
using Application.Validation.Dto;

namespace Application.Validation;

public class ValidationManager
{

    private readonly IEnumerable<IValidationRule> _validationRules;

    public ValidationManager(IEnumerable<IValidationRule> validationRules)
    {
        _validationRules = validationRules;
    }

    public async Task<ProjectResult> ValidateAsync(CreateReservationCommand command)
    {

        foreach (var rule in _validationRules)
        {
            var result = await rule.ValidateAsync(command);
            if (!result.IsValid)
            {
                return result;
            }
        }
        return new ProjectResult { IsValid = true, Message = "Geçerli" };
    }
}
