using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Publisher.Implementations.AsyncCommunicators
{
    internal class RabbitMQCommunicator : IAsyncCommunicator
    {
        private readonly ConnectionFactory _rabbitConnection;

        public RabbitMQCommunicator([FromServices] ConnectionFactory rabbitConnection)
        {
            _rabbitConnection = rabbitConnection;

            SetupRabbitMQ();
        }

        private void SetupRabbitMQ()
        {
            var opt = _rabbitConnection.Ssl;
            if (opt != null && opt.Enabled)
            {
                opt.Version = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

                opt.AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateChainErrors |
                    SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateNotAvailable;
            }
        }

        public async Task Send(string message, string queueName)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            await Task.Run(() =>
            {
                using var connection = _rabbitConnection.CreateConnection();
                using var channel = connection.CreateModel();
                CreateQueue(channel, queueName);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            });
        }

        public async Task<T> Receive<T>(string queueName)
        {
            using (var connection = _rabbitConnection.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                CreateQueue(channel, queueName);
                var data = channel.BasicGet(queueName, true);
                if (data != null)
                {
                    var content = Encoding.UTF8.GetString(data.Body.ToArray());
                    return await Task.FromResult(JsonConvert.DeserializeObject<T>(content));
                }
            }

            return default;
        }

        protected void CreateQueue(IModel channel, string queueName)
        {
            channel.QueueDeclare(queue: queueName,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
        }
    }
}
