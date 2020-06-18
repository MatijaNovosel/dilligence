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
using tvz2api_cqrs.Custom.Attributes;

namespace tvz2api_cqrs.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CourseTaskController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public CourseTaskController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [HttpGet]
    // [AuthorizeBelongsToCourse]
    public async Task<IActionResult> Get(int courseId, string name, bool showOverdue, bool showActive)
    {
      var specification = new CourseTaskSpecification(courseId, name, showOverdue, showActive);
      var result = await _queryBus.ExecuteAsync(new CourseTaskQuery(specification));
      var count = await _queryBus.ExecuteAsync(new CourseTaskTotalQuery(specification));
      return Ok(new PageableCollection<CourseTaskQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("details/{id}")]
    // [AuthorizeBelongsToCourse]
    public async Task<IActionResult> GetDetails(int id, int courseId)
    {
      var result = await _queryBus.ExecuteAsync(new CourseTaskDetailsQuery()
      {
        Id = id
      });
      return Ok(result);
    }

    [HttpGet("attempts/{id}")]
    // [AuthorizeBelongsToCourse]
    public async Task<IActionResult> GetAttempts(int id, int courseId)
    {
      var result = await _queryBus.ExecuteAsync(new CourseTaskAttemptsQuery()
      {
        Id = id,
        CourseId = courseId
      });
      return Ok(result);
    }

    [HttpGet("attempts/details/{id}")]
    // [AuthorizeBelongsToCourse]
    public async Task<IActionResult> GetAttemptDetails(int id, int courseId)
    {
      var result = await _queryBus.ExecuteAsync(new CourseTaskAttemptDetailsQuery()
      {
        Id = id,
        CourseId = courseId
      });
      return Ok(result);
    }

    [HttpPost("new-attempt")]
    // [AuthorizeBelongsToCourse]
    public async Task<IActionResult> NewAttempt(int courseId, [FromForm] CourseTaskSubmitAttemptCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("edit-attempt")]
    // [AuthorizeBelongsToCourse]
    public async Task<IActionResult> EditAttempt(int courseId, [FromForm] CourseTaskEditAttemptCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPost("grade-attempt")]
    // [AuthorizeCoursePrivilege(PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageTasks, PrivilegeEnum.CanGradeTasks)]
    public async Task<IActionResult> GradeAttempt(int courseId, CourseTaskGradeAttemptCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpDelete("{id}")]
    // [AuthorizeCoursePrivilege(PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageTasks, PrivilegeEnum.CanDeleteTasks)]
    public async Task<IActionResult> CreateNew(int id, int courseId)
    {
      await _commandBus.ExecuteAsync(new CourseTaskDeleteCommand()
      {
        CourseId = courseId,
        Id = id
      });
      return Ok();
    }

    [HttpPost]
    // [AuthorizeCoursePrivilege(PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageTasks, PrivilegeEnum.CanCreateTasks)]
    public async Task<IActionResult> CreateNew(int courseId, [FromForm] CourseTaskCreateCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut]
    // [AuthorizeCoursePrivilege(PrivilegeEnum.CanManageCourse, PrivilegeEnum.CanManageTasks, PrivilegeEnum.CanCreateTasks)]
    public async Task<IActionResult> Update(int courseId, [FromForm] CourseTaskUpdateCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }
  }
}