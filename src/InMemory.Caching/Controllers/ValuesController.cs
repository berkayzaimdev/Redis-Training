using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //[HttpGet("set/{name}")]
        //public void Set(string name)
        //{
        //    _memoryCache.Set("name", name);
        //}

        //[HttpGet]
        //public string Get()
        //{
        //    if (_memoryCache.TryGetValue<string>("name",out string name))
        //    {
        //        return name.Substring(1);
        //    }
        //    return "";
        //}

        [HttpGet("set")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30), // cache'in maksimum hayatta kalma süresi
                SlidingExpiration = TimeSpan.FromSeconds(5) // bu süre zarfında cache'e erişilirse, cache'in süresini belirtilen miktar kadar uzat. örnekte 5 saniye dolduğu an erişim yoksa, cache'i temizler. en fazla 30 saniyeye kadar uzayabilir.
            });
        }

        [HttpGet("get")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
