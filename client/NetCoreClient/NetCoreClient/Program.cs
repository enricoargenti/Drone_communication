using NetCoreClient.Sensors;
using NetCoreClient.Protocols;
using System.Text.Json;
using System.Text.Json.Serialization;

// define sensors
List<ISensorInterface> sensors = new();

// drone id initialization
string droneId = "1";

// every sensor measurement (new) is put in a list of sensors
sensors.Add(new VirtualBatteryLevelSensor());
sensors.Add(new VirtualHeightSensor());
sensors.Add(new VirtualPositionSensor());
sensors.Add(new VirtualSpeedSensor());

// define protocol
//IProtocolInterface protocol = new Http("http://localhost:8011/drones/1");
IProtocolInterface protocol = new Mqtt("127.0.0.1");

// send data to server
while (true)
{
    // cycles on every sensor value
    foreach (ISensorInterface sensor in sensors)
    {
        var sensorValue = sensor.ToJson();

        string topicSuffix = sensor.GetType().Name; //Al suo posto creare un metodo GetName in interfaccia e classi sotto

        protocol.Send(sensorValue, droneId, topicSuffix);

        Console.WriteLine("Data sent: " + sensorValue + "\n");


        Thread.Sleep(1000);
    }

}