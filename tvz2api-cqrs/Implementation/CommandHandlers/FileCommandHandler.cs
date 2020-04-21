using tvz2api_cqrs.Common;
using tvz2api_cqrs.Implementation.Commands;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.QueryModels;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Infrastructure.CommandHandlers;
using tvz2api_cqrs.Infrastructure.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class FileCommandHandler :
    ICommandHandlerAsync<FileUploadCommand, List<int>>,
    ICommandHandlerAsync<FileDeleteCommand>,
    ICommandHandlerAsync<FileUploadSidebarCommand, List<int>>
  {
    private readonly tvz2Context _context;

    public FileCommandHandler(tvz2Context context)
    {
      _context = context;
    }

    public async Task<ICommandResult<List<int>>> HandleAsync(FileUploadCommand command)
    {
      List<tvz2api_cqrs.Models.File> newFiles = new List<tvz2api_cqrs.Models.File>();

      foreach (var file in command.Files)
      {
        using (var ms = new MemoryStream())
        {
          file.CopyTo(ms);
          var fileBytes = ms.ToArray();

          var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
          fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + fileName.Substring(fileName.LastIndexOf(".")).ToLower();

          newFiles.Add(new tvz2api_cqrs.Models.File
          {
            Name = Path.GetFileName(fileName),
            ContentType = file.ContentType,
            Data = fileBytes
          });
        }
      }

      await _context.File.AddRangeAsync(newFiles);
      await _context.SaveChangesAsync();

      return CommandResult<List<int>>.Success(new List<int>(newFiles.Select(x => x.Id)));
    }

    public async Task<ICommandResult<List<int>>> HandleAsync(FileUploadSidebarCommand command)
    {
      List<tvz2api_cqrs.Models.File> newFiles = new List<tvz2api_cqrs.Models.File>();

      foreach (var file in command.Files)
      {
        using (var ms = new MemoryStream())
        {
          file.CopyTo(ms);
          var fileBytes = ms.ToArray();

          var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
          fileName = fileName.Substring(0, fileName.LastIndexOf(".")) + fileName.Substring(fileName.LastIndexOf(".")).ToLower();

          newFiles.Add(new tvz2api_cqrs.Models.File
          {
            Name = Path.GetFileName(fileName),
            ContentType = file.ContentType,
            Data = fileBytes
          });
        }
      }

      await _context.File.AddRangeAsync(newFiles);
      await _context.SaveChangesAsync();

      newFiles.ForEach(x =>
      {
        _context.SidebarContentFile.Add(new SidebarContentFile()
        {
          SidebarContentId = command.SidebarId,
          FileId = x.Id
        });
      });

      await _context.SaveChangesAsync();

      return CommandResult<List<int>>.Success(new List<int>(newFiles.Select(x => x.Id)));
    }

    public async Task HandleAsync(FileDeleteCommand command)
    {
      var sidebarContents = _context.SidebarContentFile.Where(x => x.FileId == command.Id);
      _context.SidebarContentFile.RemoveRange(sidebarContents);

      var file = await _context.File.Where(x => x.Id == command.Id).FirstOrDefaultAsync();
      _context.File.Remove(file);

      await _context.SaveChangesAsync();
    }
  }
}
