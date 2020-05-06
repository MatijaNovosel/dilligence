using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tvz2api_cqrs.Implementation.Queries;
using tvz2api_cqrs.Infrastructure.QueryHandlers;
using tvz2api_cqrs.Models;
using tvz2api_cqrs.Models.DTO;
using tvz2api_cqrs.QueryModels;
using System;
using System.Security.Claims;

namespace tvz2api_cqrs.Implementation.QueryHandlers
{
  public class CourseQueryHandler :
    IQueryHandlerAsync<CourseQuery, List<CourseQueryModel>>,
    IQueryHandlerAsync<CourseTotalQuery, int>,
    IQueryHandlerAsync<UserCourseTotalQuery, int>,
    IQueryHandlerAsync<UserCourseQuery, List<UserCourseDetailsDTO>>,
    IQueryHandlerAsync<CourseNotificationsQuery, List<NotificationQueryModel>>,
    IQueryHandlerAsync<CourseDetailsQuery, CourseDetailsQueryModel>,
    IQueryHandlerAsync<CourseSidebarQuery, List<SidebarContentDTO>>
  {
    private readonly lmsContext _context;

    public CourseQueryHandler(lmsContext context)
    {
      _context = context;
    }

    public async Task<CourseDetailsQueryModel> HandleAsync(CourseDetailsQuery query)
    {
      var course = await _context.Course
        .Where(t => t.Id == query.Id)
        .Select(t => new CourseDetailsQueryModel
        {
          Id = t.Id,
          Name = t.Name,
          Ects = t.Ects,
          Course = t.Specialization.Name,
          SidebarContents = t.SidebarContent
            .Where(x => x.CourseId == query.Id)
            .Select(x => new SidebarContentDTO
            {
              Id = x.Id,
              Naslov = x.Title,
              Files = x.SidebarContentFile
                .Where(y => y.SidebarContentId == x.Id)
                .Select(y => new FileDTO
                {
                  Id = y.File.Id,
                  Name = y.File.Name,
                  ContentType = y.File.ContentType,
                  Data = y.File.Data
                })
                .ToList()
            })
            .ToList()
        })
        .FirstOrDefaultAsync();
      return course;
    }

    public async Task<List<SidebarContentDTO>> HandleAsync(CourseSidebarQuery query)
    {
      var sidebarContent = await _context.SidebarContent
        .Where(x => x.CourseId == query.Id)
        .Select(x => new SidebarContentDTO
        {
          Id = x.Id,
          Naslov = x.Title,
          Files = x.SidebarContentFile
            .Where(y => y.SidebarContentId == x.Id)
            .Select(y => new FileDTO
            {
              Id = y.File.Id,
              Name = y.File.Name,
              ContentType = y.File.ContentType,
              Data = y.File.Data
            })
            .ToList()
        })
        .ToListAsync();
      return sidebarContent;
    }

    public async Task<List<UserCourseDetailsDTO>> HandleAsync(UserCourseQuery query)
    {
      var course = await _context.Course
        .Include(t => t.Subscription)
        .ThenInclude(t => t.User)
        .ThenInclude(t => t.ImageFile)
        .Where(t => t.Id == query.Id)
        .FirstOrDefaultAsync();
      var users = course.Subscription
        .Select(t => new UserCourseDetailsDTO
        {
          Id = t.User.Id,
          Name = t.User.Name,
          Surname = t.User.Surname,
          Username = t.User.Username,
          Email = t.User.Email,
          Created = t.User.Created,
          Picture = t.User.ImageFile != null ? Convert.ToBase64String(t.User.ImageFile.Data) : null,
          Admin = t.User.Id == course.MadeById
        })
        .ToList();
      return users;
    }

    public async Task<List<CourseQueryModel>> HandleAsync(CourseQuery query)
    {
      var courses = await _context.Course
        .Where(query.Specification.Predicate)
        .Select(t => new CourseQueryModel
        {
          Id = t.Id,
          Name = t.Name,
          Ects = t.Ects,
          Subscribed = t.Subscription.Any(x => x.UserId == query.Specification.UserId),
          SpecializationId = t.SpecializationId
        })
        .ToListAsync();
      return courses;
    }

    public async Task<int> HandleAsync(CourseTotalQuery query)
    {
      var count = await _context.Course
        .Where(query.Specification.Predicate)
        .Select(t => new CourseQueryModel
        {
          Id = t.Id,
          Name = t.Name,
          Ects = t.Ects,
          Subscribed = t.Subscription.Any(x => x.UserId == query.Specification.UserId),
          SpecializationId = t.SpecializationId
        }).CountAsync();
      return count;
    }

    public async Task<int> HandleAsync(UserCourseTotalQuery query)
    {
      var count = await _context.User
        .Where(t => t.Subscription.Any(x => x.CourseId == query.Id))
        .CountAsync();
      return count;
    }

    public async Task<List<NotificationQueryModel>> HandleAsync(CourseNotificationsQuery query)
    {
      var courses = await _context.Notification
        .Select(t => new NotificationQueryModel
        {
          Id = t.Id,
          Description = t.Description,
          Title = t.Title,
          Course = t.Course.Name,
          SubmittedAt = t.SubmittedAt,
          SubmittedBy = $"{t.SubmittedBy.Name} {t.SubmittedBy.Surname}",
          Color = t.Color,
          ExpiresAt = t.ExpiresAt,
          Archived = t.ExpiresAt <= DateTime.Now,
          CourseId = t.CourseId
        })
        .Where(x => x.CourseId == query.Id && x.Archived == query.ShowArchived || (query.ShowArchived && query.ShowNonArchived))
        .ToListAsync();
      return courses;
    }
  }
}