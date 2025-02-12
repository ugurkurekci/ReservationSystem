using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Infrastructure.RabbitMQ;

public class RabbitMQClientPublisherService : IRabbitMQClientPublisherService
{

    private readonly IRabbitMQClientConnectionService _rabbitMQClientConnectionService;
    private IModel channel;
    private IConnection connection;

    public RabbitMQClientPublisherService(IRabbitMQClientConnectionService rabbitMQClientConnectionService)
    {
        _rabbitMQClientConnectionService = rabbitMQClientConnectionService;
    }

    public bool QueuePublish(object dto, string exchange, string routingKey)
    {

        connection = _rabbitMQClientConnectionService.GetConnection();
        channel = _rabbitMQClientConnectionService.GetModel(connection);
        string message = JsonConvert.SerializeObject(dto);
        byte[] body = Encoding.UTF8.GetBytes(message);


        try
        {
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, body: body);

            Console.WriteLine(" [x] Sent {0}", message);

        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message, true);
        }
        finally
        {
            channel?.Close();
            channel?.Dispose();

            connection?.Close();
            connection?.Dispose();
        }
        return true;
    }

}