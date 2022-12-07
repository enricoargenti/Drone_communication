using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSender
{
    internal class Consumer
    {
        private ManualResetEvent _resetEvent = null;
        private IConnection _connection;
        private IModel _channel;

        public Consumer(string url, ManualResetEvent resetEvent)
        {
            _resetEvent = resetEvent;

            // create a connection and open a channel, dispose them when done
            var factory = new ConnectionFactory
            {
                Uri = new Uri(url)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void ConsumeQueue()
        {
            // ensure that the queue exists before we access it
            var queueName = "queue1";
            bool durable = true;
            bool exclusive = false;
            bool autoDelete = false;

            _channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);

            var consumer = new EventingBasicConsumer(_channel);

            // add the message receive event
            consumer.Received += (model, deliveryEventArgs) =>
            {
                var body = deliveryEventArgs.Body.ToArray();
                // convert the message back from byte[] to a string
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("** Received message: {0} by Consumer thread **", message);
                // ack the message, ie. confirm that we have processed it
                // otherwise it will be requeued a bit later
                _channel.BasicAck(deliveryEventArgs.DeliveryTag, false);
            };

            // start consuming
            _ = _channel.BasicConsume(consumer, queueName);

            // Wait for the reset event and clean up when it triggers
            _resetEvent.WaitOne();
            Console.WriteLine("CancelEvent recieved, shutting down Consumer");
            _channel?.Close();
            _channel = null;
            _connection?.Close();
            _connection = null;
        }
    }
}
