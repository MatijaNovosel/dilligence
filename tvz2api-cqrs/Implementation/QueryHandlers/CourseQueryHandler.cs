/* using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class CourseQueryHandler:
    IQueryHandlerAsync<CourseQuery, List<KolegijQueryModel>>,
    IQueryHandlerAsync<CourseTotalQuery, int>,
    IQueryHandlerAsync<StudentKolegijTotalQuery, int>,
    IQueryHandlerAsync<StudentKolegijQuery, List<StudentQueryModel>>,
    IQueryHandlerAsync<CourseDetailsQuery, KolegijDetailsQueryModel>,
    IQueryHandlerAsync<CourseSidebarQuery, List<SidebarContentDTO>>
    {
      private readonly tvz2Context _context;

      public CourseQueryHandler(tvz2Context context)
      {
        _context = context;
      }

      public async Task<KolegijDetailsQueryModel> HandleAsync(CourseDetailsQuery query)
      {
        var kolegij = await _context.Kolegij
          .Where(t => t.Id == query.Id)
          .Select(t => new KolegijDetailsQueryModel
          {
            Id = t.Id,
              Naziv = t.Naziv,
              Ects = t.Ects,
              Smjer = t.Smjer.Naziv,
              Studenti = t.StudentKolegij
              .Select(x => new StudentDTO
              {
                Ime = x.Student.Ime,
                  Prezime = x.Student.Prezime,
                  Jmbag = x.Student.Jmbag,
                  ImagePath = x.Student.ImagePath,
                  Email = x.Student.Email
              })
              .ToList(),
              SidebarContents = t.SidebarContent
              .Where(x => x.KolegijId == query.Id)
              .Select(x => new SidebarContentDTO
              {
                Id = x.Id,
                  Naslov = x.Naslov,
                  Files = x.SidebarContentFile
                  .Where(y => y.SidebarContentId == x.Id)
                  .Select(y => new FileDTO
                  {
                    Id = y.File.Id,
                      Naziv = y.File.Naziv,
                      ContentType = y.File.ContentType,
                      Data = y.File.Data
                  })
                  .ToList()
              })
              .ToList()
          })
          .FirstOrDefaultAsync();
        return kolegij;
      }

      public async Task<List<SidebarContentDTO>> HandleAsync(CourseSidebarQuery query)
      {
        var sidebarContent = await _context.SidebarContent
          .Where(x => x.KolegijId == query.Id)
          .Select(x => new SidebarContentDTO
          {
            Id = x.Id,
              Naslov = x.Naslov,
              Files = x.SidebarContentFile
              .Where(y => y.SidebarContentId == x.Id)
              .Select(y => new FileDTO
              {
                Id = y.File.Id,
                  Naziv = y.File.Naziv,
                  ContentType = y.File.ContentType,
                  Data = y.File.Data
              })
              .ToList()
          })
          .ToListAsync();
        return sidebarContent;
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

      public async Task<List<KolegijQueryModel>> HandleAsync(CourseQuery query)
      {
        var kolegiji = await _context.Kolegij
          .Where(query.Specification.Predicate)
          .Select(t => new KolegijQueryModel
          {
            Id = t.Id,
              Naziv = t.Naziv,
              Ects = t.Ects,
              SmjerId = t.SmjerId
          })
          .ToListAsync();
        return kolegiji;
      }

      public async Task<int> HandleAsync(CourseTotalQuery query)
      {
        var count = await _context.Kolegij
          .Where(query.Specification.Predicate)
          .Select(t => new KolegijQueryModel
          {
            Id = t.Id,
              Naziv = t.Naziv,
              Ects = t.Ects,
              SmjerId = t.SmjerId
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
} */