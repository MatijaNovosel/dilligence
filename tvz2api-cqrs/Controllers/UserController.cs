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
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Custom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace tvz2api_cqrs.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public UserController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(string name = null)
    {
      // var queryOptions = QueryOptionsExtensions.GetFromRequest(Request);
      var result = await _queryBus.ExecuteAsync(new UserGetAllQuery());
      return Ok(result);
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
    public async Task<IActionResult> GetUserDetails(int id)
    {
      var result = await _queryBus.ExecuteAsync(new UserDetailsQuery() { Id = id });
      return Ok(result);
    }

    [HttpPut("update-blacklist")]
    public async Task<IActionResult> UpdateBlacklist(UserUpdateBlacklistCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpGet("blacklist/{id}")]
    public async Task<IActionResult> GetBlacklist(int id)
    {
      var result = await _queryBus.ExecuteAsync(new UserBlacklistQuery() { UserId = id });
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

    [HttpPut("personal")]
    public async Task<IActionResult> UpdatePersonalInformation(UserUpdatePersonalInformationCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("update-privileges")]
    public async Task<IActionResult> UpdatePrivileges(UserUpdatePrivilegesCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("update-general")]
    public async Task<IActionResult> UpdateGeneral(UserUpdateGeneralCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("update-specific")]
    public async Task<IActionResult> UpdateSpecific(UserUpdateSpecificCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return Ok();
    }

    [HttpPut("image/{userId}")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile picture, int userId)
    {
      var file = await _commandBus.ExecuteAsync<UserProfilePictureDTO>(new UserUploadPictureCommand(userId, picture));
      return Ok(file);
    }
  }
}