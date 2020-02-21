using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class FileQuery : IQuery<List<FileQueryModel>>
  {
    public FileQuery(int id) 
    { 
      Id = id;
    }
    public int Id { get; set; }
  }
}
