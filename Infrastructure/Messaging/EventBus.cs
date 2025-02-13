using Infrastructure.RabbitMQ;

namespace Infrastructure.Messaging;

public class EventBus : IEventBus
{
    private readonly IRabbitMQClientPublisherService _rabbitMQClientPublisherService;

    public EventBus(IRabbitMQClientPublisherService rabbitMQClientPublisherService)
    {
        _rabbitMQClientPublisherService = rabbitMQClientPublisherService;
    }

    public async Task PublishQueueAsync<T>(T @event, string exchange, string routingKey)
    {
        _rabbitMQClientPublisherService.QueuePublish(@event, exchange, routingKey);
    }
}