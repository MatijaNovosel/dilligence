using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Common
{
  public class PageableCollection<TResult>
  {
    public IEnumerable<TResult> Results { get; set; }
    public int Total { get; set; }
  }
}
