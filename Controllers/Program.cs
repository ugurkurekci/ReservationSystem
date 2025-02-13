using Application.Sagas;
using Application.Services;
using Application.Validation;
using Application.Validation.Abstracts;
using Infrastructure.Messaging;
using Infrastructure.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<IEventBus, EventBus>();

builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<ReservationSagaOrchestrator>();

builder.Services.AddScoped<ValidationManager>();


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<BalanceService>();

builder.Services.AddTransient<IValidationRule, UserExistsValidationRule>();
builder.Services.AddTransient<IValidationRule, DeviceAvailabilityValidationRule>();
builder.Services.AddTransient<IValidationRule, BalanceValidationRule>();

builder.Services.AddTransient<IRabbitMQClientConnectionService, RabbitMQClientConnectionService>();
builder.Services.AddTransient<IRabbitMQClientPublisherService, RabbitMQClientPublisherService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
