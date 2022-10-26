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
            double longitude = (Random.Next(10, 20000));
            double latitude = (Random.Next(10, 20000));
            return new Position(longitude, latitude);
        }
       

        public string ToJson()
        {
            double longitude = (Random.Next(10, 20000));
            double latitude = (Random.Next(10, 20000));
            return JsonSerializer.Serialize(new Position(longitude, latitude));
        }
    }
}
