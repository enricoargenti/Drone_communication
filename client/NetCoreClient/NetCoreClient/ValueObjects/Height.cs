namespace NetCoreClient.ValueObjects
{
    internal class Height
    {
        public double Value { get; private set; }

        public Height(double value)
        {
            this.Value = value;
        }

    }
}
