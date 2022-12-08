using RabbitMQ.Client;
using System.Text;
using StackExchange.Redis;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Nodes;
using System.Text.Json;
using NetCoreClient.Packets; //This projects depends on NetCoreClient
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using DataSender;

class Program
{
    // URL of my AMQP server on CloudAMQP
    static readonly string _url = "amqps://erzcddln:Noq08Uww2NHycbvZHuarnYOZbYdigSQG@whale.rmq.cloudamqp.com/erzcddln";
    static ManualResetEvent _quitEvent = new ManualResetEvent(false);

    public static async Task<int> Main(string[] args)
    {
        var url = _url;
        if (args.Length > 0)
            url = args[0];

        // Creates a connection and open a channel, dispose them when done
        var factory = new ConnectionFactory
        {
            Uri = new Uri(url)
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        // Creates two different queues:
        // the first one to store all data,
        // the second one to store only drones positions
        var queue1 = "drones.measurements";
        var queue2 = "drones.measurements.positions";

        // queues parameters
        bool durable = true;
        bool exclusive = false;
        bool autoDelete = false;

        // Declares queues with all their parameters
        channel.QueueDeclare(queue1, durable, exclusive, autoDelete, null);
        channel.QueueDeclare(queue2, durable, exclusive, autoDelete, null);

        // Declares a new Exchange named "myExchange" with type topic
        channel.ExchangeDeclare("myExchange", "topic");

        // Declares a new binding for queue1 ("drones.measurements")
        var routingKey1 = queue1;
        channel.QueueBind(queue1, "myExchange", routingKey1);

        // Declares a new binding for queue2 ("drones.measurements.positions")
        var routingKey2 = queue2;
        channel.QueueBind(queue2, "myExchange", routingKey2);

        //--------------------------------------------------------------------------------------------------
        // Connection to Redis
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        const string key = "packet"; //forse da sostituire con droneId perché nel pacchetto non c'è

        string? packet = "pending";
        //--------------------------------------------------------------------------------------------------
        while (true)
        {
            // Extracts a packet from the queue contained in Redis
            packet = await db.ListLeftPopAsync(key);

            if (packet != null)
            {
                //Console.WriteLine("Extracted from Redis: " + packet);

                // Data put on the queue must be a byte array
                var data = Encoding.UTF8.GetBytes(packet);


                // Inserts all data on queue1 ("drones.measurements")
                channel.BasicPublish("myExchange", routingKey1, null, data);
                Console.WriteLine("Published message {0} on {1}\n", queue1, packet);

                // Inserts all data on queue2 ("drones.measurements.positions")
                // if data type is Position
                PacketChecker pc = new PacketChecker();
                if (pc.Check(packet, "Position"))
                {
                    channel.BasicPublish("myExchange", routingKey2, null, data);
                    Console.WriteLine("Published message {0} on {1}\n", queue2, packet);
                }
            }

            
        }
    }
}