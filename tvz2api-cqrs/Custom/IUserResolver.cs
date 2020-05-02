using System.Security.Claims;
using tvz2api_cqrs.Enumerations;

namespace tvz2api_cqrs.Custom
{
  public interface IUserResolver
  {
    public ClaimsPrincipal User { get; }
    bool UserBelongsToCourse(int courseId);
    bool CheckPrivileges(params PrivilegeEnum[] requestedPrivileges);
  }
}