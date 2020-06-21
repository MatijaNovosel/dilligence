using System.Security.AccessControl;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Queries;
using tvz2api_cqrs.Infrastructure.Specifications;
using tvz2api_cqrs.Implementation.Specifications;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.Queries
{
  public class UserQuery : IQuery<List<UserQueryModel>>
  {
    public UserQuery() { }
    public UserQuery(UserSpecification specification)
    {
      Specification = specification;
    }
    public UserSpecification Specification { get; set; }
  }

  public class UserGetAllQuery : IQuery<List<UserQueryModel>>
  {
    public UserGetAllQuery() { }
  }

  public class UserTotalQuery : IQuery<int>
  {
    public UserTotalQuery(UserSpecification specification)
    {
      Specification = specification;
    }
    public UserTotalQuery() { }
    public UserSpecification Specification { get; set; }
  }

  public class UserChatQuery : IQuery<List<UserChatQueryModel>>
  {
    public UserChatQuery() { }
    public int Id { get; set; }
  }

  public class UserSettingsQuery : IQuery<UserSettingsQueryModel>
  {
    public UserSettingsQuery() { }
    public int Id { get; set; }
  }

  public class UserDetailsQuery : IQuery<UserDetailsDTO>
  {
    public UserDetailsQuery() { }
    public int Id { get; set; }
  }

  public class UserBlacklistQuery : IQuery<List<BlacklistDTO>>
  {
    public UserBlacklistQuery() { }
    public int UserId { get; set; }
  }

  public class UserSubscriptionQuery : IQuery<List<int>>
  {
    public UserSubscriptionQuery(int id)
    {
      Id = id;
    }
    public int Id { get; set; }
  }
}
