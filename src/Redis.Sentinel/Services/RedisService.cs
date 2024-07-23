using StackExchange.Redis;
using System.Net;

namespace Redis.Sentinel.Services
{
    public class RedisService
    {
        static ConfigurationOptions sentinelOptions => new()
        {
            EndPoints =
            {
                { "localhost", 6383 },
                { "localhost", 6384 },
                { "localhost", 6385 }
            },
            CommandMap = CommandMap.Sentinel,
            AbortOnConnectFail = false
        };

        static ConfigurationOptions masterOptions => new()
        {
            AbortOnConnectFail = false
        };

        public static async Task<IDatabase> RedisMasterDatabase()
        {
            ConnectionMultiplexer sentinelConnection = await ConnectionMultiplexer.SentinelConnectAsync(sentinelOptions);
            EndPoint masterEndPoint = null;
            
            foreach(var endpoint in sentinelConnection.GetEndPoints()) // sentinel endpointlere erişebiliyoruz
            {
                IServer server = sentinelConnection.GetServer(endpoint); // sentinel sunucusunu endpoint üzerinden al
                if (server.IsConnected) // sunucu bağlı mı?
                {
                    masterEndPoint = await server.SentinelGetMasterAddressByNameAsync("mymaster"); // name'den master adresini al. master adı neyse onu yaz. buradaki name, sentinel.conf dosyasıyla direkt ilişkili
                    break;
                }
            }

            var localMasterIP = masterEndPoint.ToString() switch // docker ile çalıştığımız için mecburen tek tek yazmalıyız. docker'deki container IP'den local'deki karşılığını elde ettik
            {
                "172.17.0.2" => "localhost:6379",
                "172.17.0.3" => "localhost:6380",
                "172.17.0.4" => "localhost:6381",
                "172.17.0.5" => "localhost:6382",
            };

            ConnectionMultiplexer masterConnection = await ConnectionMultiplexer.ConnectAsync(localMasterIP); // local ip'sinden master bağlantısını al
            IDatabase database = masterConnection.GetDatabase(); // database'i al

            return database;
        }
    }
}
