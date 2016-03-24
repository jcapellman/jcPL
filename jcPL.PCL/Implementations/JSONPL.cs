using System.Threading.Tasks;

using jcPL.PCL.Transports;

using Newtonsoft.Json;

namespace jcPL.PCL.Implementations {
    public class JSONPL : BasePL {
        public JSONPL(BasePS persistantImplementation) : base(persistantImplementation) { }
         
        public override ReturnSet<T> Get<T>(string key) {
            var result = _persistantImplementation.Get<string>(key);

            return !result.HasValue ? new ReturnSet<T>() : new ReturnSet<T>(JsonConvert.DeserializeObject<T>(result.Value));
        }

        public override bool Put<T>(string key, T obj) {
            var jsonObj = JsonConvert.SerializeObject(obj);

            return _persistantImplementation.Put(key, jsonObj);
        }

        public override async Task<ReturnSet<T>> GetAsync<T>(string key) {
            var result = await _persistantImplementation.GetAsync<string>(key);

            return !result.HasValue ? new ReturnSet<T>() : new ReturnSet<T>(JsonConvert.DeserializeObject<T>(result.Value));
        }

        public override async Task<bool> PutAsync<T>(string key, T obj) {
            var jsonObj = JsonConvert.SerializeObject(obj);

            return await _persistantImplementation.PutAsync(key, jsonObj);
        }
    }
}