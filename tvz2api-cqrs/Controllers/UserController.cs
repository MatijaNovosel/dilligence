using tvz2api_cqrs.Models;
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
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Custom;

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : CustomController
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public UserController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string name = null)
    {
      // var queryOptions = QueryOptionsExtensions.GetFromRequest(Request);
      var specification = new UserSpecification(name);
      var result = await _queryBus.ExecuteAsync(new UserQuery(specification));
      var count = await _queryBus.ExecuteAsync(new UserTotalQuery(specification));
      return Ok(new PageableCollection<UserQueryModel>() { Results = result, Total = count });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSettings(int id)
    {
      var result = await _queryBus.ExecuteAsync(new UserSettingsQuery() { Id = id });
      return Ok(result);
    }

    [HttpGet("chat/{id}")]
    public async Task<IActionResult> GetChats(int id)
    {
      var result = await _queryBus.ExecuteAsync(new UserChatQuery() { Id = id });
      return Ok(result);
    }

    [HttpGet("subscription/{id}")]
    public async Task<IActionResult> GetPretplata(int id)
    {
      var result = await _queryBus.ExecuteAsync(new UserSubscriptionQuery(id));
      return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Subscribe(UserSubscribeCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("unsubscribe")]
    public async Task<IActionResult> Unsubscribe(UserUnsubscribeCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("settings")]
    public async Task<IActionResult> UpdateSettings(UserUpdateSettingsCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }
  }
}