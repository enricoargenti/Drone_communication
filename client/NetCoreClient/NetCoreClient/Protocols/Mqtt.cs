using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;

namespace NetCoreClient.Protocols
{
    internal class Mqtt : IProtocolInterface
    {
        private const string TOPIC_PREFIX = "iot2022test";
        private IMqttClient mqttClient;
        private string endpoint;


        public Mqtt(string endpoint)
        {
            this.endpoint = endpoint;

            Connect().GetAwaiter().GetResult();
        }
        private async Task<MqttClientConnectResult> Connect()
        {
            var factory = new MqttFactory();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(this.endpoint)
                .Build();

            mqttClient = factory.CreateMqttClient();

            return await mqttClient.ConnectAsync(options, CancellationToken.None);
        }

        public async void Send(string data, string drone_id, string topic_suffix)
        {

            string topic = $"{TOPIC_PREFIX}/{drone_id}/{topic_suffix}";
            Console.WriteLine("topic: " + topic);

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(data)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce) //QoS = 2
                .Build();

            await mqttClient.PublishAsync(message, CancellationToken.None);
        }

        // receives commands from cloud
        public async void Receive()//usare una callback qui dentro (ad una funzione esterna)
        {
            string topic = $"{TOPIC_PREFIX}/commands/#";
            Console.WriteLine("subscribed to topic: " + topic);
            
            await mqttClient.SubscribeAsync(topic);
        }

    }
}