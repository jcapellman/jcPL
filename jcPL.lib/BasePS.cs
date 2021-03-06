﻿using System;
using System.Threading.Tasks;

using jcPL.lib.Transports;

using Newtonsoft.Json;

namespace jcPL.lib {
    public abstract class BasePS {
        public abstract Task<ReturnSet<T>> GetAsync<T>(string dataKey);

        public abstract ReturnSet<T> Get<T>(string dataKey);

        public abstract Task<bool> PutAsync<T>(string dataKey, T fileData, bool replaceExisting = true);

        public abstract bool Put<T>(string dataKey, T fileData, bool replaceExisting = true);

        public abstract Guid Put<T>(T fileData);

        public abstract Task<Guid> PutAsync<T>(T fileData);

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