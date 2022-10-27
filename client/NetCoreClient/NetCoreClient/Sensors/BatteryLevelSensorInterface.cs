using NetCoreClient.ValueObjects;

namespace NetCoreClient.Sensors
{
    internal interface IBatteryLevelSensorInterface
    {
        BatteryLevel BatteryLevel();

    }
}
