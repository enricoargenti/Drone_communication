using NetCoreClient.ValueObjects;
using System.Text.Json;

namespace NetCoreClient.Sensors
{
    internal class VirtualPositionSensor : IPositionSensorInterface, ISensorInterface
    {
        private readonly Random Random;

        public VirtualPositionSensor()
        {
            Random = new Random();
        }

        // Creates a randomly generated value to simulate the drone sensor
        public Position Position()
        {
            string type = "Position";
            double longitude = (Random.Next(10, 20000));
            double latitude = (Random.Next(10, 20000));
            DateTime time = DateTime.Now;
            return new Position(type, longitude, latitude, time);
        }

        // Returns the sensor value in JSON format
        public string ToJson()
        {
            return JsonSerializer.Serialize(Position());
        }

        // Returns the sensor type in string format
        public string GetType()
        {
            return "Position";
        }
    }
}
