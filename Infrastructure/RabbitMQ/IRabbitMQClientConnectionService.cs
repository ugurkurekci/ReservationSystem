using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ;

public interface IRabbitMQClientConnectionService
{

    IConnection GetConnection();

    IModel GetModel(IConnection connection);

}