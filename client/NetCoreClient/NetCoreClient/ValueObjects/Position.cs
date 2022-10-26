namespace NetCoreClient.ValueObjects
{
    internal class Position
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Position(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

    }
}
