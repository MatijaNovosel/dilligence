using tvz2api_cqrs.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using System;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using Microsoft.AspNetCore.Http;

namespace tvz2api_cqrs.Custom
{
  public class UserResolver : IUserResolver
  {
    private readonly IHttpContextAccessor _accessor;
    private readonly lmsContext _context;

    public ClaimsPrincipal User
    {
      get
      {
        return _accessor?.HttpContext?.User as ClaimsPrincipal;
      }
    }

    public UserPrivilegeDTO Privileges
    {
      get
      {
        return JsonConvert.DeserializeObject<UserPrivilegeDTO>(User.FindFirst("Privileges").Value);
      }
    }

    public UserResolver(IHttpContextAccessor accessor, lmsContext context)
    {
      _accessor = accessor;
      _context = context;
    }

    public bool HasGeneralPrivilege(params PrivilegeEnum[] requestedPrivileges)
    {
      var generalPrivileges = Privileges.GeneralPrivileges;
      return generalPrivileges.Any(p => requestedPrivileges.ToList().Contains((PrivilegeEnum)p));
    }

    public bool HasCoursePrivilege(int courseId, params PrivilegeEnum[] requestedPrivileges)
    {
      var course = Privileges.Courses.FirstOrDefault(x => x.Id == courseId);
      if (course == null)
      {
        return false;
      }
      return course.Privileges.Any(p => requestedPrivileges.ToList().Contains((PrivilegeEnum)p));
    }

    public bool UserBelongsToCourse(int courseId)
    {
      int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
      return _context.Subscription.Any(x => x.UserId == userId && x.CourseId == courseId);
    }
  }
}