namespace NetCoreClient.ValueObjects
{
    internal class BatteryLevel
    {
        public int Value { get; private set; }

        public BatteryLevel(int value)
        {
            this.Value = value;
        }

    }
}
