using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;
using System;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class ChatQueryHandler :
    IQueryHandlerAsync<ChatDetailsQuery, ChatQueryModel>,
    IQueryHandlerAsync<ChatAvailableUsersQuery, List<UserQueryModel>>
  {
    private readonly lmsContext _context;

    public ChatQueryHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task<ChatQueryModel> HandleAsync(ChatDetailsQuery query)
    {
      var chat = await _context.Chat
        .Where(t => t.Id == query.Id)
        .Select(t => new ChatQueryModel
        {
          Id = t.Id,
          Messages = t.Message
            .Where(x => x.ChatId == query.Id)
            .Select(x => new MessageDTO()
            {
              Id = x.Id,
              Content = x.Content,
              SentAt = x.SentAt,
              UserId = x.UserId
            }).ToList()
        })
        .FirstOrDefaultAsync();
      return chat;
    }

    public async Task<List<UserQueryModel>> HandleAsync(ChatAvailableUsersQuery query)
    {
      var users = await _context.User
        .Include(t => t.ImageFile)
        .Where(t =>
          t.Id != query.Id &&
          !t.ChatFirstParticipant.Any(x => x.FirstParticipantId == query.Id || x.SecondParticipantId == query.Id) &&
          !t.ChatSecondParticipant.Any(x => x.FirstParticipantId == query.Id || x.SecondParticipantId == query.Id)
        )
        .Select(t => new UserQueryModel
        {
          Id = t.Id,
          Created = t.Created,
          Username = t.Username,
          Picture = t.ImageFile != null ? Convert.ToBase64String(t.ImageFile.Data) : null,
        })
        .ToListAsync();
      return users;
    }
  }
}