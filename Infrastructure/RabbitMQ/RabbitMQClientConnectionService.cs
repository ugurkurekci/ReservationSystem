using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMQ;
public class RabbitMQClientConnectionService : IRabbitMQClientConnectionService
{

    private readonly IConfiguration _configuration;

    public RabbitMQClientConnectionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IConnection GetConnection()
    {

        try
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                TopologyRecoveryEnabled = false
            };

            // Get the connection string from appsettings.json
            var connection_configuration = _configuration.GetSection("RabbitMQConnection");
            _configuration.GetSection("RabbitMQConnection").Bind(connectionFactory);
            return connectionFactory.CreateConnection();
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public IModel GetModel(IConnection connection)
    {

        try
        {
            return connection.CreateModel();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

}