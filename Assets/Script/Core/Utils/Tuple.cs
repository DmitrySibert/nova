using System.Collections.Generic;

namespace Core.Utils
{
    public class Tuple<T, V>
    {
        readonly T first;
        readonly V second;

        public Tuple(T first, V second)
        {
            this.first = first;
            this.second = second;
        }

        public T First { get { return first; } }
        public V Second { get { return second; } }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = first.GetHashCode() * prime + result;
            result = second.GetHashCode() * prime + result;

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }
            return Equals((Tuple<T, V>)obj);
        }

        public bool Equals(Tuple<T, V> other)
        {
            return other.first.Equals(first) && other.second.Equals(second);
        }
    }
}
