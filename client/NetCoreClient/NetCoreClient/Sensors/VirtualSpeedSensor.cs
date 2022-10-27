using NetCoreClient.ValueObjects;
using System.Text.Json;

namespace NetCoreClient.Sensors
{
    class VirtualSpeedSensor : ISpeedSensorInterface, ISensorInterface
    {
        private readonly Random Random;

        public VirtualSpeedSensor()
        {
            Random = new Random();
        }

        public Speed Speed()
        {
            string type = "Speed";
            int speed = Random.Next(100);
            DateTime time = DateTime.Now;

            return new Speed(type, speed, time);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(Speed());
        }
    }
}
