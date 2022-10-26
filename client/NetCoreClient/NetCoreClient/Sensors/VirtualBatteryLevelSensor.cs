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

        public int BatteryLevel()
        {
            return new BatteryLevel(Random.Next(0, 100)).Value;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(new BatteryLevel(Random.Next(0, 100)).Value);
        }
    }
}
