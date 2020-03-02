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
  public class StudentQueryHandler :
    IQueryHandlerAsync<StudentDetailsQuery, StudentQueryModel>
  {
    private readonly tvz2Context _context;

    public StudentQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<StudentQueryModel> HandleAsync(StudentDetailsQuery query)
    {
      var student = await _context.Student
        .Where(t => t.Id == query.Id)
        .Select(t => new StudentQueryModel
        {
          Id = t.Id,
          Email = t.Email,
          Ime = t.Ime,
          Prezime = t.Prezime,
          Jmbag = t.Jmbag
        })
        .FirstOrDefaultAsync();
      return student;
    }
  }
}
