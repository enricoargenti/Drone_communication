using NetCoreClient.ValueObjects;
using System.Text.Json;

namespace NetCoreClient.Sensors
{
    internal class VirtualHeightSensor : IHeightSensorInterface, ISensorInterface
    {
        private readonly Random Random;

        public VirtualHeightSensor()
        {
            Random = new Random();
        }

        public double Height()
        {
            double height = Random.Next(10, 20000);
            return new Height(height).Value;
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(new Speed(Random.Next(100)));
        }
    }
}
