using tvz2api_cqrs.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using System;
using tvz2api_cqrs.Models;
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

    public UserResolver(IHttpContextAccessor accessor, lmsContext context)
    {
      _accessor = accessor;
      _context = context;
    }

    public bool CheckPrivileges(params PrivilegeEnum[] requestedPrivileges)
    {
      int[] privileges = JsonConvert.DeserializeObject<int[]>(User.FindFirst("Privileges").Value);
      if (!privileges.Any(userPrivilege => requestedPrivileges.ToList().Contains((PrivilegeEnum)userPrivilege)))
      {
        return false;
      }
      return true;
    }
    public bool UserBelongsToCourse(int courseId)
    {
      int userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
      if (!_context.Subscription.Any(x => x.UserId == userId && x.CourseId == courseId))
      {
        return false;
      }
      return true;
    }
  }
}