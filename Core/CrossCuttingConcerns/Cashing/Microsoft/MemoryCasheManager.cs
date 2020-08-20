using Core.Utilities.Interceptors.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Cashing.Microsoft
{
    public class MemoryCasheManager : ICasheManager
    {
        private IMemoryCache _cashe;
        public MemoryCasheManager()
        {
            _cashe = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object data, int duration)
        {
            _cashe.Set(key, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _cashe.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cashe.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _cashe.TryGetValue(key, out _);  //varmı yokmu kontrol et.
        }

        public void Remove(string key)
        {
            _cashe.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cashe) as dynamic;


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {

                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);


                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

        }
    }
}
