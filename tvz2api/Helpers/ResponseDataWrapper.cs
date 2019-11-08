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
        public int Total { get; set; }

        public ResponseDataWrapper(T Results)
        {
            this.Results = Results;
            Total = Results.Count;
        }
    }
}
