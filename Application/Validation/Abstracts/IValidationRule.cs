using Application.Commands;
using Application.Validation.Dto;

namespace Application.Validation.Abstracts;

public interface IValidationRule
{

    Task<ProjectResult> ValidateAsync(CreateReservationCommand command);

}