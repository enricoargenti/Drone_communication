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

        public Position Position()
        {
            string type = "Position";
            double longitude = (Random.Next(10, 20000));
            double latitude = (Random.Next(10, 20000));
            DateTime time = DateTime.Now;
            return new Position(type, longitude, latitude, time);
        }
       

        public string ToJson()
        {
            return JsonSerializer.Serialize(Position());
        }
    }
}
