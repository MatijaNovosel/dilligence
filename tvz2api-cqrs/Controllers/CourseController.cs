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

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CourseController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public CourseController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get(int? userId = null, [FromQuery(Name = "smjerIDs[]")] List<SpecializationEnum> courseIds = null, string name = null, bool subscribed = false, bool nonSubscribed = false)
    {
      // var queryOptions = QueryOptionsExtensions.GetFromRequest(Request);
      var specification = new CourseSpecification(userId, courseIds, name, subscribed, nonSubscribed);
      var result = await _queryBus.ExecuteAsync(new CourseQuery(specification));
      var count = await _queryBus.ExecuteAsync(new CourseTotalQuery(specification));
      return Ok(new PageableCollection<CourseQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUsers(int id)
    {
      var result = await _queryBus.ExecuteAsync(new UserCourseQuery(id));
      var count = await _queryBus.ExecuteAsync(new UserCourseTotalQuery(id));
      return Ok(new PageableCollection<UserCourseDetailsDTO>() { Results = result, Total = count });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
      var result = await _queryBus.ExecuteAsync(new CourseDetailsQuery(id));
      return Ok(result);
    }

    [HttpGet("notifications/{id}")]
    public async Task<IActionResult> GetNotifications(int id, bool showArchived, bool showNonArchived)
    {
      var result = await _queryBus.ExecuteAsync(new CourseNotificationsQuery() { Id = id, ShowNonArchived = showNonArchived, ShowArchived = showArchived });
      return Ok(result);
    }

    [HttpGet("sidebar/{id}")]
    public async Task<IActionResult> GetSidebarContents(int id)
    {
      var result = await _queryBus.ExecuteAsync(new CourseSidebarQuery(id));
      return Ok(result);
    }
  }
}