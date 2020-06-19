using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Custom;

namespace tvz2api_cqrs.Controllers
{
  [Route("api/auth")]
  [ApiController]
  [AllowAnonymous]
  public class AuthenticationController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public AuthenticationController(ICommandBus commandBus, IQueryBus queryBus)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(AuthenticationRegisterCommand command)
    {
      await _commandBus.ExecuteAsync(command);
      return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Register(AuthenticationLoginCommand command)
    {
      var userInfo = await _commandBus.ExecuteAsync<LoginUserDTO>(command);
      return Ok(userInfo);
    }

    [HttpGet("check-username")]
    public async Task<IActionResult> CheckUsername(string username)
    {
      var check = await _queryBus.ExecuteAsync(new AuthenticationCheckUsernameQuery() { Username = username });
      return Ok(check);
    }
  }
}