namespace jcPL.PCL {
    public abstract class BasePL {
        public abstract T Get<T>(string key);

        public abstract bool Put<T>(string key, T obj);
    }
}