namespace Infrastructure.Messaging;

public interface IEventBus
{

    Task PublishQueueAsync<T>(T @event,string exchange,string routingKey);

}