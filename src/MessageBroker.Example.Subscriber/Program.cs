using StackExchange.Redis;

ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost:1453");
ISubscriber subscriber = redis.GetSubscriber();

subscriber.SubscribeAsync("stock.*", (channel, value) =>  
{
    Console.WriteLine(value);
});

Console.Read();