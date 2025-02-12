using Application.Commands;
using Application.Services;
using Application.Validation.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{

    private readonly ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
    {

        ProjectResult reservation = await _reservationService.CreateReservationAsync(command);
        if (!reservation.IsValid)
        {
            return BadRequest(reservation.Message);
        }

        return Ok(reservation.Message);

    }
}