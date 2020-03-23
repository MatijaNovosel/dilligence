using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Enumerations;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.Messaging;
using tvz2api_cqrs.Implementation.Specifications;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using tvz2api_cqrs.Hubs;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ChatController : ControllerBase
  {
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(ICommandBus commandBus, IQueryBus queryBus, IHubContext<ChatHub> chatHub)
    {
      _commandBus = commandBus;
      _queryBus = queryBus;
      _hubContext = chatHub;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
      var chat = await _queryBus.ExecuteAsync(new ChatDetailsQuery(id));
      return Ok(chat);
    }

    [HttpGet("available/{id}")]
    public async Task<IActionResult> GetAvailableUsers(int id)
    {
      var users = await _queryBus.ExecuteAsync(new ChatAvailableUsersQuery(id));
      return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageCommand command)
    {
      var message = await _commandBus.ExecuteAsync<MessageDTO>(command);
      await this._hubContext.Clients.All.SendAsync("messageSent", message.Payload);
      return NoContent();
    }
  }
}