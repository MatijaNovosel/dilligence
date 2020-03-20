using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class ChatCommandHandler :
    ICommandHandlerAsync<SendMessageCommand, MessageDTO>
  {
    private readonly tvz2Context _context;

    public ChatCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<ICommandResult<MessageDTO>> HandleAsync(SendMessageCommand command)
    {
      Message message = new Message()
      {
        ChatId = command.ChatId,
        UserId = command.UserId,
        Content = command.Content
      };
      await _context.Message.AddAsync(message);
      await _context.SaveChangesAsync();
      return CommandResult<MessageDTO>.Success(new MessageDTO()
      {
        Id = message.Id,
        Content = message.Content,
        SentAt = message.SentAt,
        UserId = message.UserId
      });
    }
  }
}