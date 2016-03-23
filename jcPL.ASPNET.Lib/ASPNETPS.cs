using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

using jcPL.PCL;

namespace jcPL.ASPNET.Lib {
    public class ASPNETPS : BasePS {
        private readonly ObjectCache _cache = MemoryCache.Default;

        public override Task<T> GetAsync<T>(string dataKey) {
            throw new NotImplementedException();
        }

        public override T Get<T>(string dataKey) {
            if (!_cache.Contains(dataKey)) {
                return default(T);
            }

            return (T)Convert.ChangeType(_cache[dataKey], typeof (T));
        }

        public override Task<bool> PutAsync<T>(string dataKey, T fileData, bool replaceExisting = true) {
            throw new NotImplementedException();
        }

        public override bool Put<T>(string dataKey, T fileData, bool replaceExisting = true) {
            if (_cache.Contains(dataKey)) {
                if (replaceExisting) {
                    _cache[dataKey] = fileData;
                }
            } else {
                _cache.Add(dataKey, fileData, DateTimeOffset.MaxValue);
            }

            return true;
        }
    }
}