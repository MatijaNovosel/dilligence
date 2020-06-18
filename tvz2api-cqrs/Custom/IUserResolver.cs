using System.Security.Claims;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Models.DTO;
using System.Collections.Generic;

namespace tvz2api_cqrs.Custom
{
  public interface IUserResolver
  {
    ClaimsPrincipal User { get; }
    int Id { get; }
    bool UserBelongsToCourse(int courseId);
    bool HasGeneralPrivilege(List<PrivilegeEnum> requestedPrivileges);
    bool HasCoursePrivilege(int courseId, List<PrivilegeEnum> requestedPrivileges);
  }
}