namespace NetCoreClient.ValueObjects
{
    internal class Height
    {
        public string Type { get; set; }
        public double Value { get; private set; }
        public DateTime Time { get; private set; }

        public Height(string type, double value, DateTime time)
        {
            this.Type = type;
            this.Value = value;
            this.Time = time;
        }

    }
}
