using System.Collections.Generic;
using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Specifications;
using tvz2api_cqrs.Infrastructure.Queries;
using tvz2api_cqrs.Infrastructure.Specifications;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class ChatDetailsQuery : IQuery<ChatQueryModel>
  {
    public ChatDetailsQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }

  public class ChatAvailableUsersQuery : IQuery<List<UserQueryModel>>
  {
    public ChatAvailableUsersQuery(ChatSpecification specification)
    {
      Specification = specification;
    }
    public ChatSpecification Specification { get; set; }
  }
}