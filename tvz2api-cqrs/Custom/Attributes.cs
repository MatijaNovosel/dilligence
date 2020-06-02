using System.Security.Claims;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Models.DTO;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using tvz2api_cqrs.Custom;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace tvz2api_cqrs.Custom.Attributes
{
  public class AuthorizeCoursePrivilege : Attribute, IAuthorizationFilter
  {
    private readonly PrivilegeEnum[] _requestedPrivileges;

    public AuthorizeCoursePrivilege(params PrivilegeEnum[] requestedPrivileges)
    {
      _requestedPrivileges = requestedPrivileges;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var query = context.HttpContext.Request.Query.Where(x => x.Key == "courseId").Take(1).ToList();

      if (query.Count == 0 || query[0].Value.First() == null)
      {
        context.Result = new UnauthorizedResult();
      }

      var privileges = JsonConvert.DeserializeObject<UserPrivilegeDTO>(context.HttpContext.User.FindFirst("Privileges").Value);
      var course = privileges.Courses.FirstOrDefault(x => x.Id == Int32.Parse(query[0].Value.First()));

      if (course == null || !course.Privileges.Any(p => _requestedPrivileges.Contains((PrivilegeEnum)p)))
      {
        context.Result = new UnauthorizedResult();
      }
    }
  }

  public class AuthorizeBelongsToCourse : Attribute, IAuthorizationFilter
  {
    public AuthorizeBelongsToCourse() { }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      var query = context.HttpContext.Request.Query.Where(x => x.Key == "courseId").Take(1).ToList();

      if (query.Count == 0 || query[0].Value.First() == null)
      {
        context.Result = new UnauthorizedResult();
      }

      var subscriptions = JsonConvert.DeserializeObject<List<int>>(context.HttpContext.User.FindFirst("Subscriptions").Value);

      if (subscriptions == null || subscriptions.Count == 0 || !subscriptions.Any(p => p == Int32.Parse(query[0].Value.First())))
      {
        context.Result = new UnauthorizedResult();
      }
    }
  }
}