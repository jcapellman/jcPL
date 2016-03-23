using System.Threading.Tasks;

using Newtonsoft.Json;

namespace jcPL.PCL.Implementations {
    public class JSONPL : BasePL {
        public JSONPL(BasePS persistantImplementation) : base(persistantImplementation) { }
         
        public override T Get<T>(string key) {
            return JsonConvert.DeserializeObject<T>(_persistantImplementation.Get<string>(key));
        }

        public override bool Put<T>(string key, T obj) {
            var jsonObj = JsonConvert.SerializeObject(obj);

            return _persistantImplementation.Put(key, jsonObj);
        }

        public override async Task<T> GetAsync<T>(string key) {
            return JsonConvert.DeserializeObject<T>(await _persistantImplementation.GetAsync<string>(key));
        }

        public override async Task<bool> PutAsync<T>(string key, T obj) {
            var jsonObj = JsonConvert.SerializeObject(obj);

            return await _persistantImplementation.PutAsync(key, jsonObj);
        }
    }
}