using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

using jcPL.PCL;
using jcPL.PCL.Transports;

namespace jcPL.ASPNET.Lib {
    public class ASPNETPS : BasePS {
        private readonly ObjectCache _cache = MemoryCache.Default;

        public override Task<ReturnSet<T>> GetAsync<T>(string dataKey) {
            throw new NotImplementedException();
        }

        public override ReturnSet<T> Get<T>(string dataKey) {
            return !_cache.Contains(dataKey) ? new ReturnSet<T>() : new ReturnSet<T>((T)Convert.ChangeType(_cache[dataKey], typeof (T)));
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

        private Guid generateUniqueGuid() {
            var unique = false;
            var tmpGuid = Guid.NewGuid();

            do {
                if (!_cache.Contains(tmpGuid.ToString())) {
                    unique = true;
                } else {
                    tmpGuid = Guid.NewGuid();
                }               
            } while (!unique);

            return tmpGuid;            
        }

        public override Guid Put<T>(T fileData) {
            var returnGuid = generateUniqueGuid();

            Put(returnGuid.ToString(), fileData);

            return returnGuid;
        }

        public override Task<Guid> PutAsync<T>(T fileData) { throw new NotImplementedException(); }
    }
}