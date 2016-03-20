namespace jcPL.PCL {
    public abstract class BasePL {
        protected BasePS _persistantImplementation;

        protected BasePL(BasePS persistantImplementation) {
            _persistantImplementation = persistantImplementation;
        }

        public abstract T Get<T>(string key);

        public abstract bool Put<T>(string key, T obj);
    }
}