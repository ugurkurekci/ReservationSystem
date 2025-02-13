using Application.Commands;
using Application.Sagas;
using Application.Validation;
using Application.Validation.Dto;
using Domain.Events;
using Domain.Models;
using Infrastructure.Messaging;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReservationService
    {
        private readonly IEventBus _eventBus;
        private readonly ValidationManager _validationManager;
        private readonly ReservationSagaOrchestrator _reservationSagaOrchestrator;

        public ReservationService(IEventBus eventBus, ValidationManager validationManager, ReservationSagaOrchestrator reservationSagaOrchestrator)
        {
            _eventBus = eventBus;
            _validationManager = validationManager;
            _reservationSagaOrchestrator = reservationSagaOrchestrator;
        }

        public async Task<ProjectResult> CreateReservationAsync(CreateReservationCommand command)
        {
            if (!IsValidCommand(command, out var validationMessage))
                return ErrorResult(validationMessage);

            var validationResult = await _validationManager.ValidateAsync(command);
            if (!validationResult.IsValid)
                return ErrorResult(validationResult.Message);

            var reservation = new Reservation
            {
                UserId = command.UserId,
                DeviceId = command.DeviceId,
                ReservationDate = DateTime.UtcNow
            };

            // **Veritabanına kaydedilmesi gerekiyor, burada simüle**
            reservation.Id = SaveReservationToDatabase(reservation);

            var reservationEvent = new ReservationCreatedEvent(reservation.Id, reservation.UserId, reservation.DeviceId, reservation.ReservationDate);
            await PublishEventAsync(reservationEvent, "reservation_events_direct", "reservation.start");

            await _reservationSagaOrchestrator.StartSagaAsync(reservationEvent);

            return SuccessResult("Rezervasyon başarıyla oluşturuldu.");
        }

        private bool IsValidCommand(CreateReservationCommand command, out string message)
        {
            if (command == null)
            {
                message = "Geçersiz istek";
                return false;
            }

            if (command.UserId <= 0 || command.DeviceId <= 0)
            {
                message = "Geçersiz kullanıcı veya cihaz bilgisi";
                return false;
            }

            message = string.Empty;
            return true;
        }

        private async Task PublishEventAsync(object @event, string exchange, string routingKey)
        {
            try
            {
                await _eventBus.PublishQueueAsync(@event, exchange, routingKey);
                Console.WriteLine($"Event published: {@event.GetType().Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error publishing event: {ex.Message}");
            }
        }

        private int SaveReservationToDatabase(Reservation reservation)
        {
            // **Gerçek bir veritabanı kaydı olmalı**
            return new Random().Next(100000, 999999);
        }

        private ProjectResult SuccessResult(string message) => new ProjectResult { IsValid = true, Message = message };

        private ProjectResult ErrorResult(string message) => new ProjectResult { IsValid = false, Message = message };
    }
}
