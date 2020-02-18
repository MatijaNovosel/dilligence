using tvz2api_cqrs.Common;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class KolegijQueryHandler : IQueryHandlerAsync<KolegijQuery, List<KolegijQueryModel>>
  {
    private readonly tvz2Context _context;

    public KolegijQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<List<KolegijQueryModel>> HandleAsync(KolegijQuery query)
    {
      var kolegiji = await _context.Kolegij.Select(t => new KolegijQueryModel { Id = t.Id, Naziv = t.Naziv, Ects = t.Ects }).ToListAsync();
      return kolegiji;
    }
  }
}
