namespace Infrastructure.RabbitMQ;

public interface IRabbitMQClientPublisherService
{
    bool PublishEvent(object dto, string exchange);
    bool QueuePublish<T>(T message, string exchange, string routingKey);
}