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

        public BatteryLevel BatteryLevel()
        {
            string type = "BatteryLevel";
            int batteryLevel = (Random.Next(0, 100));
            DateTime time = DateTime.Now;

            return new BatteryLevel(type, batteryLevel, time);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(BatteryLevel());
        }
    }
}
