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
    private readonly int _courseId;
    
    public AuthorizeCoursePrivilege(int courseId, params PrivilegeEnum[] requestedPrivileges)
    {
      _requestedPrivileges = requestedPrivileges;
      _courseId = courseId;
    }

    public void OnAuthorization(AuthorizationFilterContext context) 
    {
      var privileges = JsonConvert.DeserializeObject<UserPrivilegeDTO>(context.HttpContext.User.FindFirst("Privileges").Value);
      var course = privileges.Courses.FirstOrDefault(x => x.Id == _courseId);

      if (course == null || !course.Privileges.Any(p => _requestedPrivileges.Contains((PrivilegeEnum)p)))
      {
        context.Result = new UnauthorizedResult();
      }
    }
  }
}