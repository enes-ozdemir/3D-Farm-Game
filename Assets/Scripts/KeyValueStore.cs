using System.Collections.Generic;

namespace Farm.Core
{
    public class KeyValueStore<T, R>
    {
        private Dictionary<T, R> store = new Dictionary<T, R>();

        public KeyValueStore(Dictionary<T, R> store)
        {
            this.store = store;
        }

        public R get(T key)
        {
            R value;
            store.TryGetValue(key, out value);
            return value;
        }

        public int count()
        {
            return store.Count;
        }

        public R getOrDefault(T key, R defaultValue)
        {
            R value;
            store.TryGetValue(key, out value);
            if (value == null)
            {
                return defaultValue;
            }
            return value;
        }

        public void set(T key, R value)
        {
            if (store.ContainsKey(key))
            {
                store.Remove(key);
                store.Add(key, value);
            }
            else
            {
                store.Add(key, value);
            }
        }

        public bool exist(T key)
        {
            return store.ContainsKey(key);
        }

        public bool exist(R value)
        {
            return store.ContainsValue(value);
        }
    }
}