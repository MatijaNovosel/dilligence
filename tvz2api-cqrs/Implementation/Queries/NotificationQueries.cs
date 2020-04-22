using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class NotificationQuery : IQuery<List<NotificationQueryModel>>
  {
    public NotificationQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }
}