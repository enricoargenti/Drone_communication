using StackExchange.Redis;



// Connection to Redis
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();


const string key = "packet"; //forse da sostituire con droneId perché nel pacchetto non c'è

string? result = "pending";

while(result != null)
{
     result = await db.ListLeftPopAsync(key);
    Console.WriteLine("Direttamente da Redis: " + result);
}