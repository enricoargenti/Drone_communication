using NetCoreClient.Sensors;
using NetCoreClient.Protocols;

// define sensors
List<ISensorInterface> sensors = new();

sensors.Add(new VirtualBatteryLevelSensor());
sensors.Add(new VirtualHeightSensor());
sensors.Add(new VirtualPositionSensor());
sensors.Add(new VirtualSpeedSensor());

// define protocol
IProtocolInterface protocol = new Http("http://localhost:8011/drones/1");

// send data to server
while (true)
{

    foreach (ISensorInterface sensor in sensors)
    {
        var sensorValue = sensor.ToJson();

        protocol.Send(sensorValue);

        Console.WriteLine("Data sent: " + sensorValue);

        Thread.Sleep(1000);
    }

}