using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class ChatQueryHandler :
    IQueryHandlerAsync<ChatDetailsQuery, ChatQueryModel>,
    IQueryHandlerAsync<ChatAvailableUsersQuery, List<KorisnikQueryModel>>
  {
    private readonly tvz2Context _context;

    public ChatQueryHandler(tvz2Context context)
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

    public async Task<List<KorisnikQueryModel>> HandleAsync(ChatAvailableUsersQuery query)
    {
      var users = await _context.Korisnik
        .Where(t => 
          t.Id != query.Id &&
          !t.ChatFirstParticipant.Any(x => x.FirstParticipantId == query.Id || x.SecondParticipantId == query.Id) &&
          !t.ChatSecondParticipant.Any(x => x.FirstParticipantId == query.Id || x.SecondParticipantId == query.Id)
        )
        .Select(t => new KorisnikQueryModel
        {
          Id = t.Id,
          Created = t.Created,
          Username = t.Username
        })
        .ToListAsync();
      return users;
    }
  }
}
