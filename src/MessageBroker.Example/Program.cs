using StackExchange.Redis;

ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync("localhost:1453"); // bağlanacağımız adresi yazarız

ISubscriber subscriber = redis.GetSubscriber(); // bağlantı üzerinden bir subscriber oluşturulur

const string message = "test deneme";
await subscriber.PublishAsync("stock.*", message); // Consumer

await subscriber.SubscribeAsync("stock.*", (channel, message) => { // channel adını böyle bir pattern olarak verirsek; stock.apple, stock.google gibi kanallar tüketilecektir!
    Console.WriteLine(message); // kanaldan aldığımız mesajı yazdırırız
});
