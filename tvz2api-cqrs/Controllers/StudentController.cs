using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Implementation.Commands;
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

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentController : ControllerBase
  {
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;

    public StudentController(IQueryBus queryBus, ICommandBus commandBus)
    {
      _queryBus = queryBus;
      _commandBus = commandBus;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var result = await _queryBus.ExecuteAsync(new StudentDetailsQuery(id));
      return Ok(result);
    }

    [HttpGet("pretplata/{id}")]
    public async Task<IActionResult> GetPretplata(int id)
    {
      var result = await _queryBus.ExecuteAsync(new StudentPretplataQuery(id));
      return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Subscribe(StudentSubscribeCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return NoContent();
    }

    [HttpPut("unsubscribe")]
    public async Task<IActionResult> Unsubscribe(StudentUnsubscribeCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return NoContent();
    }
  }
}