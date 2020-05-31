using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Implementation.Specifications;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using tvz2api_cqrs.Custom;
using tvz2api_cqrs.Implementation.Commands;

namespace tvz2api_cqrs.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CourseController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IUserResolver _userResolver;

    public CourseController(ICommandBus commandBus, IQueryBus queryBus, IUserResolver userResolver)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
      _userResolver = userResolver;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int? userId = null, [FromQuery(Name = "specializationId[]")] List<SpecializationEnum> courseIds = null, string name = null, bool subscribed = false, bool nonSubscribed = false)
    {
      // var queryOptions = QueryOptionsExtensions.GetFromRequest(Request);
      var specification = new CourseSpecification(userId, courseIds, name, subscribed, nonSubscribed);
      var result = await _queryBus.ExecuteAsync(new CourseQuery(specification));
      var count = await _queryBus.ExecuteAsync(new CourseTotalQuery(specification));
      return Ok(new PageableCollection<CourseQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUsers(int id, string name, string surname, string username)
    {
      var specification = new CourseUserSpecification(name, surname, username, id);
      var result = await _queryBus.ExecuteAsync(new UserCourseQuery(id, specification));
      var count = await _queryBus.ExecuteAsync(new UserCourseTotalQuery(id, specification));
      return Ok(new PageableCollection<UserCourseDetailsDTO>() { Results = result, Total = count });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
      var result = await _queryBus.ExecuteAsync(new CourseDetailsQuery(id));
      return Ok(result);
    }

    [HttpGet("notifications/{id}")]
    public async Task<IActionResult> GetNotifications(int id, bool showArchived, bool showNonArchived, bool sortByNew)
    {
      var specification = new CourseNotificationsSpecification(id, showArchived, showNonArchived);
      var result = await _queryBus.ExecuteAsync(new CourseNotificationsQuery(specification)
      {
        SortByNew = sortByNew
      });
      return Ok(result);
    }

    [HttpGet("sidebar/{id}")]
    public async Task<IActionResult> GetSidebarContents(int id)
    {
      var result = await _queryBus.ExecuteAsync(new CourseSidebarQuery(id));
      return Ok(result);
    }

    [HttpPost("new-sidebar")]
    public async Task<IActionResult> CreateNewSidebar(CourseCreateNewSidebarCommand command)
    {
      if (
        !_userResolver.HasCoursePrivilege(command.CourseId, PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageCourseFiles, PrivilegeEnum.CanUploadCourseFiles) &&
        !(_userResolver.HasCoursePrivilege(command.CourseId, PrivilegeEnum.IsInvolvedWithCourse))
      )
      {
        return Unauthorized();
      }
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPost("new-discussion")]
    public async Task<IActionResult> CreateNewDiscussion(CourseCreateDiscussionCommand command)
    {
      if (!_userResolver.HasCoursePrivilege(command.CourseId, PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageDiscussion, PrivilegeEnum.CanCreateNewDiscussion))
      {
        return Unauthorized();
      }
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpDelete("delete-sidebar/{id}")]
    public async Task<IActionResult> DeleteSidebar(int id, int courseId)
    {
      if (
        !_userResolver.HasCoursePrivilege(courseId, PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageCourseFiles, PrivilegeEnum.CanDeleteCourseFiles) &&
        !(_userResolver.HasCoursePrivilege(courseId, PrivilegeEnum.IsInvolvedWithCourse))
      )
      {
        return Unauthorized();
      }
      await _commandBus.ExecuteAsync(new CourseDeleteSidebarCommand()
      {
        SidebarId = id,
        CourseId = courseId
      });
      return Ok();
    }
  }
}