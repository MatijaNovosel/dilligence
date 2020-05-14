using System.Security.Claims;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Custom
{
  public interface IUserResolver
  {
    ClaimsPrincipal User { get; }
    UserPrivilegeDTO Privileges { get; }
    bool UserBelongsToCourse(int courseId);
    bool HasGeneralPrivilege(params PrivilegeEnum[] requestedPrivileges);
    bool HasCoursePrivilege(int courseId, params PrivilegeEnum[] requestedPrivileges);
  }
}