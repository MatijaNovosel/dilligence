using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class KolegijQueryHandler : 
    IQueryHandlerAsync<KolegijQuery, List<KolegijQueryModel>>, 
    IQueryHandlerAsync<KolegijTotalQuery, int>
  {
    private readonly tvz2Context _context;

    public KolegijQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<List<KolegijQueryModel>> HandleAsync(KolegijQuery query)
    {
      var kolegiji = await _context.Kolegij
        .Select(t => new KolegijQueryModel
        {
          Id = t.Id,
          Naziv = t.Naziv,
          Ects = t.Ects,
          SmjerID = t.SmjerID
        }).ToListAsync();
      return kolegiji;
    }

    public async Task<int> HandleAsync(KolegijTotalQuery query)
    {
      var count = await _context.Kolegij
        .Select(t => new KolegijQueryModel
        {
          Id = t.Id,
          Naziv = t.Naziv,
          Ects = t.Ects
        }).CountAsync();
      return count;
    }
  }
}
