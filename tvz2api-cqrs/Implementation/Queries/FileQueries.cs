using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

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

  public class FileDownloadMultipleQuery : IQuery<FileDTO>
  {
    public FileDownloadMultipleQuery() { }
    public List<int> FileIds { get; set; }
  }
}
