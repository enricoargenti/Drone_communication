using NetCoreClient.Sensors;
using NetCoreClient.Protocols;
using System.ComponentModel;

// Define sensors
List<ISensorInterface> sensors = new();

// Drone ID initialization
Console.WriteLine("Drone id: ");
string droneId = Console.ReadLine();

// Every sensor measurement (new) is put in a list of sensors
sensors.Add(new VirtualBatteryLevelSensor());
sensors.Add(new VirtualHeightSensor());
sensors.Add(new VirtualPositionSensor());
sensors.Add(new VirtualSpeedSensor());

// Protocol definition
IProtocolInterface protocol = new Mqtt("127.0.0.1");

// Data senting to server
while (true)
{
    // Cycles on every sensor value
    foreach (ISensorInterface sensor in sensors)
    {
        // Converts sensor value to a JSON string
        var sensorValue = sensor.ToJson();

        // Gets the sensor type
        string topicSuffix = sensor.GetType(); 

        // Sends data by the specified protocol
        protocol.Send(sensorValue, droneId, topicSuffix);


        Console.WriteLine("Data sent: " + sensorValue + "\n");

        // Takes a break every second in data sending
        Thread.Sleep(1000);
    }


    // Meanwhile it keeps listening to possible commands from the cloud
    protocol.Receive(); //to do in Mqtt.cs

}