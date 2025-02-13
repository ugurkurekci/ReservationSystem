using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Infrastructure.RabbitMQ
{
    public class RabbitMQClientPublisherService : IRabbitMQClientPublisherService
    {
        private readonly IRabbitMQClientConnectionService _rabbitMQClientConnectionService;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQClientPublisherService(IRabbitMQClientConnectionService rabbitMQClientConnectionService)
        {
            _rabbitMQClientConnectionService = rabbitMQClientConnectionService;
            EnsureConnection();
        }

        private void EnsureConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _rabbitMQClientConnectionService.GetConnection();
                _channel = _rabbitMQClientConnectionService.GetModel(_connection);
            }
        }

        public bool PublishEvent(object dto, string exchange)
        {
            return QueuePublish(dto, exchange, "");
        }

        public bool QueuePublish<T>(T message, string exchange, string routingKey)
        {
            try
            {
                EnsureConnection();
                EnsureExchangeAndQueue(exchange, routingKey);

                var jsonMessage = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);

                _channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
                Console.WriteLine($" [x] Message Published to {exchange} with routing key {routingKey}: {jsonMessage}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"QueuePublish error: {ex.Message}");
                return false;
            }
        }

        private void EnsureExchangeAndQueue(string exchange, string routingKey)
        {
            _channel.ExchangeDeclare(exchange: exchange, type: "direct", durable: true, autoDelete: false);
            string queueName = $"{exchange}_{routingKey}";
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routingKey);
        }
    }
}
