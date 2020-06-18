using tvz2api_cqrs.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using System;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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

    public int Id
    {
      get
      {
        return Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
      }
    }

    public UserResolver(IHttpContextAccessor accessor, lmsContext context)
    {
      _accessor = accessor;
      _context = context;
    }

    public bool HasGeneralPrivilege(List<PrivilegeEnum> requestedPrivileges)
    {
      var generalPrivileges = _context.UserPrivilege.Where(x => x.UserId == Id).Select(x => x.PrivilegeId).ToList();
      return generalPrivileges.Any(p => requestedPrivileges.Contains((PrivilegeEnum)p));
    }

    public bool HasCoursePrivilege(int courseId, List<PrivilegeEnum> requestedPrivileges)
    {
      var coursePrivileges = _context.UserCoursePrivilege.Where(x => x.UserId == Id && x.CourseId == courseId).Select(x => x.PrivilegeId).ToList();

      if (coursePrivileges.Count() == 0)
      {
        return false;
      }

      return coursePrivileges.Any(p => requestedPrivileges.Contains((PrivilegeEnum)p));
    }

    public bool UserBelongsToCourse(int courseId)
    {
      return _context.Subscription.Any(x => x.UserId == Id && x.CourseId == courseId);
    }
  }
}