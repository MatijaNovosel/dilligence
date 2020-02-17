using tvz2api_cqrs.Common;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Implementation.QueryModels;
using tvz2api_cqrs.Implementation.StatementModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class StatementQueryHandler : IQueryHandlerAsync<TestQuery, TestQueryModel>
  {
    private readonly AppDbContext _context;

    public StatementQueryHandler(AppDbContext context)
    {
      _context = context;
    }

    public async Task<List<TestQueryModel>> HandleAsync(TestQuery query)
    {
      var tests = await _context.Test
        .Select(t => new TestQueryModel
        {
          Field = t.Field
        }).ToListAsync();

      return tests;
    }
  }
}
