namespace NetCoreClient.Sensors
{
    interface ISensorInterface
    {
        string ToJson();
        string GetType();
    }
}
