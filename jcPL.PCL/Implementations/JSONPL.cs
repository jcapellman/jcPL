using System.Collections.Generic;

using Newtonsoft.Json;

namespace jcPL.PCL.Implementations {
    public class JSONPL : BasePL {
        private readonly Dictionary<string, string> _values;
        
        public JSONPL() {  _values = new Dictionary<string, string>(); }
         
        public override T Get<T>(string key) {
            return !_values.ContainsKey(key) ? default(T) : JsonConvert.DeserializeObject<T>(_values[key]);
        }

        public override bool Put<T>(string key, T obj) {
            var jsonObj = JsonConvert.SerializeObject(obj);

            if (_values.ContainsKey(key)) {
                _values[key] = jsonObj;
            } else {
                _values.Add(key, jsonObj);
            }

            return true;
        }
    }
}