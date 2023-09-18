using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public class EnumsMapper<T, U> where T : Enum
                                   where U : Enum
    {
        readonly HashSet<Tuple<T, U>> mapper = new HashSet<Tuple<T, U>>();

        public T GetModel(U entity)
        {
            var result = mapper.Where(tuple => tuple.Item2.Equals(entity));
            if(result.Count() != 1)
            {
                return default(T);
            }
            return result.First().Item1;
        }

        public U GetEntity(T model)
        {
            var result = mapper.Where(tuple => tuple.Item1.Equals(model));
            if(result.Count() != 1)
            {
                return default(U);
            }
            return result.First().Item2;
        }

        public void Add(T model, U entity)
        {
            mapper.Add(Tuple.Create(model, entity));
        }

        public void AddRange(params Tuple<T, U>[] tuples)
        {
            foreach(var t in tuples)
            {
                Add(t.Item1, t.Item2);
            }
        }

        public EnumsMapper(params Tuple<T, U>[] tuples)
        {
            AddRange(tuples);
        }
    }
}

