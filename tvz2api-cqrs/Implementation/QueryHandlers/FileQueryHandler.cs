using tvz2api_cqrs.Models;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using tvz2api_cqrs.Models.DTO;
using System.IO;
using System.IO.Compression;
using System;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class FileQueryHandler :
    IQueryHandlerAsync<FileQuery, List<FileQueryModel>>,
    IQueryHandlerAsync<FileDownloadMultipleQuery, FileDTO>
  {
    private readonly lmsContext _context;

    public FileQueryHandler(lmsContext context)
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
          Name = t.Name,
          ContentType = t.ContentType,
          Data = t.Data
        })
        .ToListAsync();
      return files;
    }

    public async Task<FileDTO> HandleAsync(FileDownloadMultipleQuery query)
    {
      var files = await _context.File
        .Where(t => query.FileIds.Contains(t.Id))
        .Select(t => new FileDTO
        {
          Id = t.Id,
          Name = t.Name,
          ContentType = t.ContentType,
          Data = t.Data
        })
        .ToListAsync();

      var memoryStream = new MemoryStream();

      using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
      {
        foreach (var file in files)
        {
          using (var mr = new MemoryStream(file.Data))
          {
            ZipArchiveEntry entry = archive.CreateEntry(file.Name);
            using (Stream entryStream = entry.Open())
            {
              mr.Position = 0;
              mr.CopyTo(entryStream);
            }
          }
        }
      }

      memoryStream.Position = 0;
      byte[] bytes = memoryStream.ToArray();
      await memoryStream.DisposeAsync();

      return new FileDTO()
      {
        ContentType = "application/zip",
        Data = bytes,
        Name = DateTime.Now.ToString("dd.MM.yyyy - HHmm") + ".zip"
      };
    }
  }
}
