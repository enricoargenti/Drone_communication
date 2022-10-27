namespace NetCoreClient.ValueObjects
{
    internal class BatteryLevel
    {
        public string Type { get; set; }
        public int Value { get; private set; }
        public DateTime Time { get; private set; }

        public BatteryLevel(string type, int value, DateTime time)
        {
            this.Type = type;
            this.Value = value;
            this.Time = time;
        }

    }
}
