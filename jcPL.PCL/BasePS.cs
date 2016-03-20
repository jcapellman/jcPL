using System.Threading.Tasks;

using Newtonsoft.Json;

namespace jcPL.PCL {
    public abstract class BasePS {
        public abstract Task<T> Get<T>(string fileName);

        public abstract Task<bool> Put<T>(string fileName, T fileData, bool replaceExisting = true);

        protected static byte[] GetBytesFromT<T>(T obj) {
            var jsonStr = GetJSONStringFromT(obj);

            var bytes = new byte[jsonStr.Length * sizeof(char)];

            System.Buffer.BlockCopy(jsonStr.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        protected static T GetObjectFromBytes<T>(byte[] bytes) {
            var chars = new char[bytes.Length / sizeof(char)];

            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);

            return JsonConvert.DeserializeObject<T>(new string(chars));
        }

        protected static string GetJSONStringFromT<T>(T obj) { return JsonConvert.SerializeObject(obj); }

        protected static T GetObjectFromJSONString<T>(string str) { return JsonConvert.DeserializeObject<T>(str); }
    }
}