using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class ChatCommandHandler:
    ICommandHandlerAsync<SendMessageCommand, MessageDTO>,
    ICommandHandlerAsync<CreateNewChatCommand, NewChatDTO>
    {
      private readonly tvz2Context _context;

      public ChatCommandHandler(tvz2Context context)
      {
        _context = context;
      }

      public async Task<ICommandResult<NewChatDTO>> HandleAsync(CreateNewChatCommand command)
      {
        Chat chat = new Chat()
        {
          FirstParticipantId = command.FirstParticipantId,
          SecondParticipantId = command.SecondParticipantId
        };
        await _context.Chat.AddAsync(chat);
        await _context.SaveChangesAsync();
        return CommandResult<NewChatDTO>.Success(new NewChatDTO()
        {
          Id = chat.Id
        });
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