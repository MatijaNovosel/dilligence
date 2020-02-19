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
  public class KolegijQueryHandler :
    IQueryHandlerAsync<KolegijQuery, List<KolegijQueryModel>>,
    IQueryHandlerAsync<KolegijTotalQuery, int>,
    IQueryHandlerAsync<StudentKolegijTotalQuery, int>,
    IQueryHandlerAsync<StudentKolegijQuery, List<StudentQueryModel>>,
    IQueryHandlerAsync<KolegijDetailsQuery, KolegijDetailsQueryModel>
  {
    private readonly tvz2Context _context;

    public KolegijQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<KolegijDetailsQueryModel> HandleAsync(KolegijDetailsQuery query)
    {
      var kolegij = await _context.Kolegij
        .Where(t => t.Id == query.Id)
        .Select(t => new KolegijDetailsQueryModel
        {
          Id = t.Id,
          Naziv = t.Naziv,
          Ects = t.Ects,
          Smjer = t.Smjer.Naziv,
          Studenti = _context.Student
            .Where(x => x.StudentKolegij.Any(y => y.KolegijId == query.Id))
            .Select(x => new StudentDTO
            {
              Id = x.Id,
              Jmbag = x.Jmbag,
              Ime = x.Ime,
              Prezime = x.Prezime,
              Email = x.Email
            })
            .ToList(),
          SidebarContents = t.SidebarContent
            .Where(x => x.KolegijId == query.Id)
            .Select(x => new SidebarContentDTO {
              Id = x.Id,
              Naslov = x.Naslov,
              Files = null
            })
            .ToList()
        })
        .FirstOrDefaultAsync();
      return kolegij;
    }

    public async Task<List<StudentQueryModel>> HandleAsync(StudentKolegijQuery query)
    {
      var studenti = await _context.Student
        .Where(t => t.StudentKolegij.Any(x => x.KolegijId == query.Id))
        .Select(t => new StudentQueryModel
        {
          Id = t.Id,
          Jmbag = t.Jmbag,
          Ime = t.Ime,
          Prezime = t.Prezime,
          Email = t.Email
        })
        .ToListAsync();
      return studenti;
    }

    public async Task<List<KolegijQueryModel>> HandleAsync(KolegijQuery query)
    {
      var kolegiji = await _context.Kolegij
        .Select(t => new KolegijQueryModel
        {
          Id = t.Id,
          Naziv = t.Naziv,
          Ects = t.Ects,
          Smjer = t.Smjer.Naziv
        })
        .ToListAsync();
      return kolegiji;
    }

    public async Task<int> HandleAsync(KolegijTotalQuery query)
    {
      var count = await _context.Kolegij
        .Select(t => new KolegijQueryModel
        {
          Id = t.Id,
          Naziv = t.Naziv,
          Ects = t.Ects,
          Smjer = t.Smjer.Naziv
        }).CountAsync();
      return count;
    }

    public async Task<int> HandleAsync(StudentKolegijTotalQuery query)
    {
      var count = await _context.Student
        .Where(t => t.StudentKolegij.Any(x => x.KolegijId == query.Id))
        .Select(t => new StudentQueryModel
        {
          Id = t.Id,
          Jmbag = t.Jmbag,
          Ime = t.Ime,
          Prezime = t.Prezime,
          Email = t.Email
        })
        .CountAsync();
      return count;
    }
  }
}
