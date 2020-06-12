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
  public class ExamController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public ExamController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(int userId)
    {
      var result = await _queryBus.ExecuteAsync(new ExamInProgressQuery(userId));
      return Ok(result);
    }

    [HttpGet("unfinished")]
    public async Task<IActionResult> GetUnfinishedExams(int userId, int courseId)
    {
      var result = await _queryBus.ExecuteAsync(new ExamUnfinishedQuery()
      {
        CourseId = courseId,
        UserId = userId
      });
      return Ok(result);
    }

    [HttpGet("preview")]
    public async Task<IActionResult> GetExamPreview(int examId)
    {
      var result = await _queryBus.ExecuteAsync(new ExamPreviewQuery()
      {
        ExamId = examId
      });
      return Ok(result);
    }

    [HttpGet("finished")]
    public async Task<IActionResult> GetFinishedExams(int userId, int courseId)
    {
      var result = await _queryBus.ExecuteAsync(new ExamFinishedQuery()
      {
        CourseId = courseId,
        UserId = userId
      });
      return Ok(result);
    }

    [HttpGet("unfinished/{examId}")]
    public async Task<IActionResult> GetUnfinishedExamDetails(int examId)
    {
      var result = await _queryBus.ExecuteAsync(new ExamUnfinishedDetailsQuery(examId));
      return Ok(result);
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
      var result = await _queryBus.ExecuteAsync(new ExamInProgressDetailsQuery(id));
      return Ok(result);
    }

    [HttpPut("attempt")]
    public async Task<IActionResult> Register(ExamUpdateAttemptCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartAttempt(ExamStartAttemptCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPost("enable")]
    public async Task<IActionResult> EnableExamSolving(ExamEnableSolvingCommand command, int courseId)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPost("finalize")]
    public async Task<IActionResult> FinalizeExam(ExamFinalizeCommand command, int courseId)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(ExamUpdateCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExamPreCreateCommand command)
    {
      var id = await _commandBus.ExecuteAsync<int>(command);
      return Ok(id);
    }
  }
}