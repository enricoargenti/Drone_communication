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

        public Height Height()
        {
            string type = "Height";
            double height = Random.Next(10, 20000);
            DateTime time = DateTime.Now;

            return new Height(type, height, time);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(Height());
        }
    }
}
