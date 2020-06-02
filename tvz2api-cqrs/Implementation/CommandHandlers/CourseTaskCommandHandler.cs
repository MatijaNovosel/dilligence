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
using tvz2api_cqrs.Custom;

namespace tvz2api_cqrs.Implementation.CommandHandlers
{
  public class CourseTaskCommandHandler :
    ICommandHandlerAsync<CourseTaskCreateCommand>,
    ICommandHandlerAsync<CourseTaskDeleteCommand>,
    ICommandHandlerAsync<CourseTaskUpdateCommand>,
    ICommandHandlerAsync<CourseTaskSubmitAttemptCommand>,
    ICommandHandlerAsync<CourseTaskEditAttemptCommand>,
    ICommandHandlerAsync<CourseTaskGradeAttemptCommand>
  {
    private readonly lmsContext _context;
    private readonly IConfiguration _configuration;

    public CourseTaskCommandHandler(lmsContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }

    public async Task HandleAsync(CourseTaskGradeAttemptCommand command)
    {
      var courseTaskAttempt = await _context
        .CourseTaskAttempt
        .FirstOrDefaultAsync(x => x.Id == command.AttemptId);
      courseTaskAttempt.Grade = command.Grade;
      courseTaskAttempt.GradeeComment = command.Comment;
      courseTaskAttempt.GradedById = command.GradedById;
      await _context.SaveChangesAsync();
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
        DueDate = command.DueDate,
        GradeMaximum = command.MaximumGrade
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

    public async Task HandleAsync(CourseTaskUpdateCommand command)
    {
      CourseTask courseTask = await _context
        .CourseTask
        .Include(x => x.CourseTaskAttachment)
        .FirstOrDefaultAsync(x => x.Id == command.Id);

      courseTask.Title = command.Title;
      courseTask.Description = command.Description;
      courseTask.DueDate = command.DueDate;
      courseTask.GradeMaximum = command.MaximumGrade;

      _context.CourseTaskAttachment.RemoveRange(courseTask.CourseTaskAttachment);

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

    public async Task HandleAsync(CourseTaskDeleteCommand command)
    {
      var courseTask = await _context
        .CourseTask
        .Include(t => t.CourseTaskAttachment)
        .Include(t => t.CourseTaskAttempt)
        .ThenInclude(t => t.TaskAttemptAttachment)
        .FirstOrDefaultAsync(t => t.Id == command.Id);
      _context.CourseTaskAttachment.RemoveRange(courseTask.CourseTaskAttachment);
      courseTask.CourseTaskAttempt.ToList().ForEach(x =>
      {
        _context.TaskAttemptAttachment.RemoveRange(x.TaskAttemptAttachment);
      });
      _context.CourseTaskAttachment.RemoveRange(courseTask.CourseTaskAttachment);
      _context.CourseTaskAttempt.RemoveRange(courseTask.CourseTaskAttempt);
      _context.CourseTask.Remove(courseTask);
      await _context.SaveChangesAsync();
    }

    public async Task HandleAsync(CourseTaskSubmitAttemptCommand command)
    {
      var newCourseTaskAttempt = new CourseTaskAttempt()
      {
        CourseTaskId = command.CourseTaskId,
        Description = command.Description,
        UserId = command.SubmittedById,
        SubmittedAt = DateTime.Now
      };

      await _context.CourseTaskAttempt.AddAsync(newCourseTaskAttempt);
      await _context.SaveChangesAsync();

      if (command.Files != null)
      {
        List<tvz2api_cqrs.Models.File> newFiles = new List<tvz2api_cqrs.Models.File>();

        command.Files.ForEach(file =>
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
        });

        await _context.File.AddRangeAsync(newFiles);
        await _context.SaveChangesAsync();

        newFiles.ForEach(file =>
        {
          _context.TaskAttemptAttachment.Add(new TaskAttemptAttachment()
          {
            CourseTaskAttemptId = newCourseTaskAttempt.Id,
            FileId = file.Id
          });
        });

        await _context.SaveChangesAsync();
      }
    }

    public async Task HandleAsync(CourseTaskEditAttemptCommand command)
    {
      var attempt = await _context
        .CourseTaskAttempt
        .Include(x => x.TaskAttemptAttachment)
        .FirstOrDefaultAsync(x => x.Id == command.Id);

      attempt.Description = command.Description;
      _context.TaskAttemptAttachment.RemoveRange(attempt.TaskAttemptAttachment);

      if (command.Files != null)
      {
        List<tvz2api_cqrs.Models.File> newFiles = new List<tvz2api_cqrs.Models.File>();

        command.Files.ForEach(file =>
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
        });

        await _context.File.AddRangeAsync(newFiles);
        await _context.SaveChangesAsync();

        newFiles.ForEach(file =>
        {
          _context.TaskAttemptAttachment.Add(new TaskAttemptAttachment()
          {
            CourseTaskAttemptId = attempt.Id,
            FileId = file.Id
          });
        });

        await _context.SaveChangesAsync();
      }
    }
  }
}
