using tvz2api_cqrs.Enumerations;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using System;
using tvz2api_cqrs.Models;

namespace tvz2api_cqrs.Custom
{
  public class CustomController : ControllerBase
  {
    protected tvz2Context _context;
    public CustomController(tvz2Context context)
    {
      _context = context;
    }

    private bool CheckPrivileges(params PrivilegeEnum[] requestedPrivileges)
    {
      int[] privileges = JsonConvert.DeserializeObject<int[]>(User.FindFirst("Privileges").Value);
      if (!privileges.Any(userPrivilege => requestedPrivileges.ToList().Contains((PrivilegeEnum)userPrivilege)))
      {
        return false;
      }
      return true;
    }
    private bool UserBelongsToCourse(int courseId)
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