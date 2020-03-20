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
  public class KorisnikQueryHandler :
    IQueryHandlerAsync<KorisnikQuery, List<KorisnikQueryModel>>,
    IQueryHandlerAsync<KorisnikChatQuery, List<KorisnikChatQueryModel>>,
    IQueryHandlerAsync<KorisnikTotalQuery, int>
  {
    private readonly tvz2Context _context;

    public KorisnikQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<List<KorisnikQueryModel>> HandleAsync(KorisnikQuery query)
    {
      var korisnici = await _context.Korisnik
        .Where(query.Specification.Predicate)
        .Select(t => new KorisnikQueryModel
        {
          Id = t.Id,
          Username = t.Username,
          Created = t.Created
        })
        .ToListAsync();
      return korisnici;
    }

    public async Task<int> HandleAsync(KorisnikTotalQuery query)
    {
      var count = await _context.Korisnik
        .Where(query.Specification.Predicate)
        .Select(t => new KorisnikQueryModel
        {
          Id = t.Id,
          Username = t.Username,
          Created = t.Created
        })
        .CountAsync();
      return count;
    }

    public async Task<List<KorisnikChatQueryModel>> HandleAsync(KorisnikChatQuery query)
    {
      var chats = await _context.Chat
        .Where(t => t.FirstParticipantId == query.Id || t.SecondParticipantId == query.Id)
        .Select(t => new KorisnikChatQueryModel
        {
          Id = t.Id,
          FirstParticipant = new KorisnikQueryModel() 
          { 
            Id = t.FirstParticipant.Id,
            Username = t.FirstParticipant.Username
          },
          SecondParticipant = new KorisnikQueryModel() 
          { 
            Id = t.SecondParticipant.Id,
            Username = t.SecondParticipant.Username
          }
        })
        .ToListAsync();
      return chats;
    }
  }
}
