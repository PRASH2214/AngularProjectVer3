using Cubix.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubix.Utility
{

    /// <summary>
    /// Manage the CAche in Memory or we can Use Redis Distributed here
    /// </summary>
    public class Cache
    {


        /// <summary>
        /// Serialize the JSON object
        /// </summary>
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Deserialize the JSON object
        /// </summary>
        public static HubModel Deserialize(string serialized)
        {
            if (serialized == null)
                return null;
            return JsonConvert.DeserializeObject<HubModel>(serialized);
        }
        /// <summary>
        /// Deserialize the JSON Token object
        /// </summary>
        public static TokenCacheModel DeserializeToken(string serialized)
        {
            if (serialized == null)
                return null;
            return JsonConvert.DeserializeObject<TokenCacheModel>(serialized);
        }

        /// <summary>
        /// Set Connection in Memory
        /// </summary>
        public async static Task Set(IDistributedCache _distributedCache, string key, HubModel value)
        {
            DistributedCacheEntryOptions oDistributedCacheEntryOptions = new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddHours(8) };

            await _distributedCache.SetStringAsync(key, Serialize(value), oDistributedCacheEntryOptions);//, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddHours(8)}
        }

        /// <summary>
        /// Get Connection from Memory
        /// </summary>
        public static HubModel Get(IDistributedCache _distributedCache, string key)
        {

            return Deserialize(_distributedCache.GetStringAsync(key).Result);
        }



        /// <summary>
        /// Remove Connection from Memory
        /// </summary>
        public async static Task Remove(IDistributedCache _distributedCache, string key)
        {
            await _distributedCache.RemoveAsync(key);
        }


        /// <summary>
        /// Set Token in Memory
        /// </summary>
        public async static Task SetToken(IDistributedCache _distributedCache, string key, TokenCacheModel value)
        {
            DistributedCacheEntryOptions oDistributedCacheEntryOptions = new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddHours(8) };

            await _distributedCache.SetStringAsync(key, Serialize(value), oDistributedCacheEntryOptions);//, new DistributedCacheEntryOptions { AbsoluteExpiration = DateTime.Now.AddHours(8)}
        }

        /// <summary>
        /// Get Token from Memory
        /// </summary>
        public static TokenCacheModel GetToken(IDistributedCache _distributedCache, string key)
        {

            return DeserializeToken(_distributedCache.GetStringAsync(key).Result);
        }
    }
}
