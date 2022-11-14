using NetCoreClient.Sensors;
using NetCoreClient.Protocols;

// define sensors
List<ISensorInterface> sensors = new();

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
    //MODIFICA A CASO
    //List<Object> sensorValues = new List<object>();

    foreach (ISensorInterface sensor in sensors)
    {
        var sensorValue = sensor.ToJson();

        //sensorValues.Add(sensorValue);

        protocol.Send("qualcosa", sensorValue);

        Console.WriteLine("Data sent: " + sensorValue);

        Thread.Sleep(1000);
    }

}