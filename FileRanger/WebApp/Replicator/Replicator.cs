using System.Text;
using RabbitMQ.Client;

namespace WebApp.Replicator;

public class Replicator : IReplicator{
    private readonly ConnectionFactory _conFactory;

    public Replicator(ConnectionFactory conFactory) {
        _conFactory = conFactory;
    }
    
    public void AskForReplication() {
        using var connection = _conFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "replicationQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes("");

        channel.BasicPublish(exchange: "",
            routingKey: "replicationQueue",
            basicProperties: null,
            body: body);
    }
}