using System;
using System.Threading.Tasks;

using jcPL.PCL.Transports;

namespace jcPL.PCL {
    public abstract class BasePL {
        protected BasePS _persistantImplementation;

        protected BasePL(BasePS persistantImplementation) {
            _persistantImplementation = persistantImplementation;
        }

        public abstract ReturnSet<T> Get<T>(string key);

        public abstract bool Put<T>(string key, T obj);

        public abstract Task<ReturnSet<T>> GetAsync<T>(string key);

        public abstract Task<bool> PutAsync<T>(string key, T obj);

        public abstract Task<Guid> PutAsync<T>(T obj);

        public abstract Guid Put<T>(T obj);
    }
}