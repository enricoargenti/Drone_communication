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

        // Creates a randomly generated value to simulate the drone sensor
        public Height Height()
        {
            string type = "Height";
            double height = Random.Next(10, 20000);
            DateTime time = DateTime.Now;

            return new Height(type, height, time);
        }

        // Returns the sensor value in JSON format
        public string ToJson()
        {
            return JsonSerializer.Serialize(Height());
        }

        // Returns the sensor type in string format
        public string GetType()
        {
            return "Height";
        }
    }
}
