using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvz2api.Helpers
{
    public class ResponseDataWrapper<T> where T : IEnumerable, IList
    {
        public T Results { get; set; }
        public int Count { get; set; }

        public ResponseDataWrapper(T Results)
        {
            this.Results = Results;
            Count = Results.Count;
        }
    }
}
