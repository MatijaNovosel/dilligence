using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Implementation.Specifications;
using tvz2api_cqrs.Implementation.Commands;
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
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CourseTaskController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IUserResolver _userResolver;

    public CourseTaskController(ICommandBus commandBus, IQueryBus queryBus, IUserResolver userResolver)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
      _userResolver = userResolver;
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> Get(int courseId, string name)
    {
      var specification = new CourseTaskSpecification(courseId, name);
      var result = await _queryBus.ExecuteAsync(new CourseTaskQuery(specification));
      var count = await _queryBus.ExecuteAsync(new CourseTaskTotalQuery(specification));
      return Ok(new PageableCollection<CourseTaskQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
      var result = await _queryBus.ExecuteAsync(new CourseTaskDetailsQuery()
      {
        Id = id
      });
      return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNew([FromForm] CourseTaskCreateCommand command)
    {
      if (
        !_userResolver.HasCoursePrivilege(command.CourseId, PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageTasks, PrivilegeEnum.CanCreateTasks) &&
        !(_userResolver.HasCoursePrivilege(command.CourseId, PrivilegeEnum.IsInvolvedToCourse))
      )
      {
        return Unauthorized();
      }
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] CourseTaskUpdateCommand command)
    {
      if (
        !_userResolver.HasCoursePrivilege(command.CourseId, PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageTasks, PrivilegeEnum.CanCreateTasks) &&
        !(_userResolver.HasCoursePrivilege(command.CourseId, PrivilegeEnum.IsInvolvedToCourse))
      )
      {
        return Unauthorized();
      }
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }
  }
}