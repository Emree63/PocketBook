    using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class Mapper<T, U> where T : class
                                      where U : class
    {
        readonly HashSet<Tuple<T, U>> mapper = new HashSet<Tuple<T, U>>();

        public void Reset()
        {
            mapper.Clear();
        }

        public T GetT(U entity)
        {
            var result = mapper.Where(tuple => ReferenceEquals(tuple.Item2, entity));
            if(result.Count() != 1)
            {
                return null;
            }
            return result.First().Item1;
        }

        public U GetU(T model)
        {
            var result = mapper.Where(tuple => ReferenceEquals(tuple.Item2, model));
            if(result.Count() != 1)
            {
                return null;
            }
            return result.First().Item2;
        }

        public bool AddMapping(T t, U u)
        {
            var mapping = new Tuple<T, U>(t, u);
            if(mapper.Contains(mapping)) return false;
            mapper.Add(mapping);
            return true;
        }
    }
}

