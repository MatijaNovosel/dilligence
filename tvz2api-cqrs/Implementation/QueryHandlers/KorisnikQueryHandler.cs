/* using tvz2api_cqrs.Models;
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
    IQueryHandlerAsync<KorisnikSettingsQuery, KorisnikSettingsQueryModel>,
    IQueryHandlerAsync<KorisnikTotalQuery, int>,
    IQueryHandlerAsync<KorisnikPretplataQuery, List<int>>
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

    public async Task<KorisnikSettingsQueryModel> HandleAsync(KorisnikSettingsQuery query)
    {
      var settings = await _context.UserSettings.FirstOrDefaultAsync(x => x.UserId == query.Id);
      return new KorisnikSettingsQueryModel() { DarkMode = settings.DarkMode };
    }

    public async Task<List<int>> HandleAsync(KorisnikPretplataQuery query)
    {
      var preplate = await _context.Pretplata
        .Where(t => t.StudentId == query.Id)
        .Select(t => (int)t.KolegijId)
        .ToListAsync();
      return preplate;
    }
  }
}
 */