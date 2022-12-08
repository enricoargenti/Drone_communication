using NetCoreClient.Sensors;
using NetCoreClient.Protocols;
using System.ComponentModel;
using StackExchange.Redis;

/*
 DATA COLLECTION
 */

// Define sensors
List<ISensorInterface> sensors = new();

// Drone ID initialization
Console.WriteLine("Drone id: ");
string droneId = "1"; // default drone is number 1
droneId = Console.ReadLine();

// Every sensor measurement (new) is put in a list of sensors
sensors.Add(new VirtualBatteryLevelSensor());
sensors.Add(new VirtualHeightSensor());
sensors.Add(new VirtualPositionSensor());
sensors.Add(new VirtualSpeedSensor());


// Connection to Redis
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();


const string key = "packet"; //forse da sostituire con droneId perché nel pacchetto non c'è


// Data senting to server
while (true)
{
    // Cycles on every sensor value
    foreach (ISensorInterface sensor in sensors)
    {
        // Converts sensor value to a JSON string
        var sensorValue = sensor.ToJson();

        // Sends the packet to Redis
        await db.ListRightPushAsync(key, sensorValue);

        Console.WriteLine("Data sent: " + sensorValue);


        // Takes a break every second in data sending
        Thread.Sleep(1000);
    }


}
