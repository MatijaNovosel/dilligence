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
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class CourseTaskCommandHandler :
    ICommandHandlerAsync<CourseTaskCreateCommand>
  {
    private readonly lmsContext _context;
    private readonly IConfiguration _configuration;

    public CourseTaskCommandHandler(lmsContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public async Task HandleAsync(CourseTaskCreateCommand command)
    {
      CourseTask courseTask = new CourseTask()
      {
        CourseId = command.CourseId,
        CreatedById = command.CreatedById,
        Title = command.Title,
        Description = command.Description,
        SubmittedAt = DateTime.Now,
        DueDate = command.DueDate
      };
      await _context.CourseTask.AddAsync(courseTask);
      await _context.SaveChangesAsync();

      if (command.Files != null)
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
              Data = fileBytes,
              Size = fileBytes.Length
            });
          }
        }

        await _context.File.AddRangeAsync(newFiles);
        await _context.SaveChangesAsync();

        newFiles.ForEach(x =>
        {
          _context.CourseTaskAttachment.Add(new CourseTaskAttachment()
          {
            FileId = x.Id,
            CourseTaskId = courseTask.Id
          });
        });
      }

      await _context.SaveChangesAsync();
    }
  }
}
