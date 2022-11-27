using NetCoreClient.ValueObjects;
using System.Text.Json;

namespace NetCoreClient.Sensors
{
    internal class VirtualBatteryLevelSensor : IBatteryLevelSensorInterface, ISensorInterface
    {
        private readonly Random Random;

        public VirtualBatteryLevelSensor()
        {
            Random = new Random();
        }

        // Creates a randomly generated value to simulate the drone sensor
        public BatteryLevel BatteryLevel()
        {
            string type = "BatteryLevel";
            int batteryLevel = (Random.Next(0, 100));
            DateTime time = DateTime.Now;

            return new BatteryLevel(type, batteryLevel, time);
        }
        
        // Returns the sensor value in JSON format
        public string ToJson()
        {
            return JsonSerializer.Serialize(BatteryLevel());
        }

        // Returns the sensor type in string format
        public string GetType()
        {
            return "BatteryLevel";
        }
    }
}
