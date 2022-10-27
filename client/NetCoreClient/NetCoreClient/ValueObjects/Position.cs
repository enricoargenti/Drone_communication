namespace NetCoreClient.ValueObjects
{
    internal class Position
    {
        public string Type { get; set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime Time { get; private set; }

        public Position(string type, double latitude, double longitude, DateTime time)
        {
            this.Type = type;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Time = time;
        }

    }
}
