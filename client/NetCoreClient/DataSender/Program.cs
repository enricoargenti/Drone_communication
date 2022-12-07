using RabbitMQ.Client;
using System.Text;
using DataSender;
using StackExchange.Redis;

class Program
{
    // The url of the AMQP server, should look like amqps://[username]:[password]@[instance]/[vhost]
    static readonly string _url = "amqps://erzcddln:Noq08Uww2NHycbvZHuarnYOZbYdigSQG@whale.rmq.cloudamqp.com/erzcddln";
    static ManualResetEvent _quitEvent = new ManualResetEvent(false);

    public static async Task<int> Main(string[] args)
    {
        var url = _url;
        if (args.Length > 0)
            url = args[0];

        Console.CancelKeyPress += (sender, eArgs) => {
            // set the quit event so that the Consumer will receive it and quit gracefully
            _quitEvent.Set();
            Console.WriteLine("CancelEvent received, shutting down...");
            // sleep 1 second to give Consumer time to clean up
            Thread.Sleep(1000);
        };

        // setup worker thread
        var consumer = new Consumer(url, _quitEvent);
        var consumerThread = new Thread(consumer.ConsumeQueue) { IsBackground = true };
        consumerThread.Start();

        // create a connection and open a channel, dispose them when done
        var factory = new ConnectionFactory
        {
            Uri = new Uri(url)
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        // ensure that the queue exists before we publish to it
        var queueName = "queue1";
        bool durable = true;
        bool exclusive = false;
        bool autoDelete = false;

        channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);

        //--------------------------------------------------------------------------------------------------
                                            //Prelevo da Radis
        // Connection to Redis
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        IDatabase db = redis.GetDatabase();

        const string key = "packet"; //forse da sostituire con droneId perché nel pacchetto non c'è

        string? packet = "pending";
        //--------------------------------------------------------------------------------------------------
        while (true)
        {
            //Console.WriteLine("Enter a message and publish with pressing the return key (exit with ctrl-c)");
            //var message = Console.ReadLine();
            packet = await db.ListLeftPopAsync(key);

            if (packet != null)
            {
                Console.WriteLine("Direttamente da Redis: " + packet);

                // the data put on the queue must be a byte array
                var data = Encoding.UTF8.GetBytes(packet);
                // publish to the topic exchange
                channel.ExchangeDeclare("myExchange", "topic"); //exchange set with type Topic
                var routingKey = queueName;

                channel.QueueBind("queue1", "myExchange", routingKey);

                channel.BasicPublish("myExchange", routingKey, null, data);
                Console.WriteLine("Published message {0}\n", packet);
            }

            
        }
    }
}