namespace Infrastructure.RabbitMQ;

public interface IRabbitMQClientPublisherService
{

    bool QueuePublish(object dto, string exchange, string routingKey);

}
