using System;
using System.Threading.Tasks;
using jcPL.lib.Transports;

namespace jcPL.lib.Implementations {
    public class JSONPL : BasePL {
        public JSONPL(BasePS persistantImplementation) : base(persistantImplementation) { }
         
        public override ReturnSet<T> Get<T>(string key) {
            var result = _persistantImplementation.Get<string>(key);

            return !result.HasValue ? new ReturnSet<T>() : new ReturnSet<T>(JsonConvert.DeserializeObject<T>(result.Value));
        }

        public override bool Put<T>(string key, T obj) {            
            return _persistantImplementation.Put(key, SerializeObject(obj));
        }

        public override async Task<ReturnSet<T>> GetAsync<T>(string key) {
            var result = await _persistantImplementation.GetAsync<string>(key);

            return !result.HasValue ? new ReturnSet<T>() : new ReturnSet<T>(JsonConvert.DeserializeObject<T>(result.Value));
        }

        private string SerializeObject<T>(T obj) => JsonConvert.SerializeObject(obj);                     

        public override async Task<bool> PutAsync<T>(string key, T obj) {            
            return await _persistantImplementation.PutAsync(key, SerializeObject(obj));
        }

        public override async Task<Guid> PutAsync<T>(T obj) => await _persistantImplementation.PutAsync(SerializeObject(obj));

        public override Guid Put<T>(T obj) => _persistantImplementation.Put(SerializeObject(obj));
    }
}