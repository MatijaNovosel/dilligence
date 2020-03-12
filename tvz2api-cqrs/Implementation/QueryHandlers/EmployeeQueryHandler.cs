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
  public class EmployeeQueryHandler :
    IQueryHandlerAsync<EmployeeQuery, List<EmployeeQueryModel>>
  {
    private readonly tvz2Context _context;

    public EmployeeQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<List<EmployeeQueryModel>> HandleAsync(EmployeeQuery query)
    {
      var employees = await _context.Zaposlenik
        .Select(t => new EmployeeQueryModel
        {
          Id = t.Id,
          Email = t.Email,
          Ime = t.Ime,
          Prezime = t.Prezime,
          OdjelId = t.OdjelId,
          TitulaIspred = t.TitulaIspred,
          TitulaIza = t.TitulaIza,
          VrstaZaposljenjaId = t.VrstaZaposljenjaId
        })
        .ToListAsync();
      return employees;
    }
  }
}
