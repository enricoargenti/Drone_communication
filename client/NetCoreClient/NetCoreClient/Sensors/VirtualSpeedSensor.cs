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

        // Creates a randomly generated value to simulate the drone sensor
        public Speed Speed()
        {
            string type = "Speed";
            int speed = Random.Next(100);
            DateTime time = DateTime.Now;

            return new Speed(type, speed, time);
        }

        // Returns the sensor value in JSON format
        public string ToJson()
        {
            return JsonSerializer.Serialize(Speed());
        }
        
        // Returns the sensor type in string format
        public string GetType()
        {
            return "Speed";
        }
    }
}
