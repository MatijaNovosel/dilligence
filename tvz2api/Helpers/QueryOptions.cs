using System.Collections.Generic;

namespace tvz2api.Helpers
{
    public class QueryOptionsByDesc
    {
        public QueryOptionsByDesc(string by, bool descending)
        {
            By = by;
            Descending = descending;
        }

        public string By { get; set; }
        public bool Descending { get; set; }
    }

    public class QueryOptions
    {
        public QueryOptions()
        {
            Sort = new List<QueryOptionsByDesc>();
            Group = new List<QueryOptionsByDesc>();
        }

        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<QueryOptionsByDesc> Sort { get; set; }
        public IEnumerable<QueryOptionsByDesc> Group { get; set; }
    }
}
