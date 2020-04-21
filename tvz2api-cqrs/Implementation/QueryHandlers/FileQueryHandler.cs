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
  public class FileQueryHandler :
    IQueryHandlerAsync<FileQuery, List<FileQueryModel>>
  {
    private readonly tvz2Context _context;

    public FileQueryHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<List<FileQueryModel>> HandleAsync(FileQuery query)
    {
      var files = await _context.File
        .Where(t => t.SidebarContentFile.Any(x => x.SidebarContentId == query.Id))
        .Select(t => new FileQueryModel
        {
          Id = t.Id,
          Naziv = t.Name,
          ContentType = t.ContentType,
          Data = t.Data
        })
        .ToListAsync();
      return files;
    }
  }
}
