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
  public class VijestQueryHandler :
    IQueryHandlerAsync<VijestQuery, List<VijestQueryModel>>
  {
    private readonly tvz2Context _context;

    public VijestQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<List<VijestQueryModel>> HandleAsync(VijestQuery query)
    {
      var vijesti = await _context.Vijest
        .Where(t => t.KolegijId == query.Id)
        .Select(t => new VijestQueryModel
        {
          Id = t.Id,
          Datum = t.Datum,
          KolegijId = t.KolegijId,
          Naslov = t.Naslov,
          ObjavioId = t.ObjavioId,
          Opis = t.Opis
        })
        .ToListAsync();
      return vijesti;
    }
  }
}
 */