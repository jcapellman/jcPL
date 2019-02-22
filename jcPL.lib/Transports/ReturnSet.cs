namespace jcPL.lib.Transports {
    public class ReturnSet<T> {
        public T Value { get; set; }

        public bool HasValue { get; set; }

        public ReturnSet(T objectValue) : this(objectValue, true) { }

        public ReturnSet(bool hasValue = false) : this(default(T), hasValue) { }

        public ReturnSet(T objectValue, bool hasValue = false) {
            Value = objectValue;

            HasValue = hasValue;
        } 
    }
}