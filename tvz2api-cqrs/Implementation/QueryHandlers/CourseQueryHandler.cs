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
    IQueryHandlerAsync<CourseSidebarQuery, List<SidebarContentDTO>>,
    IQueryHandlerAsync<CourseDiscussionsQuery, List<DiscussionDTO>>
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
          Specialization = t.Specialization.Name
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
          Title = x.Title,
          Files = x.SidebarContentFile
            .Where(y => y.SidebarContentId == x.Id)
            .Select(y => new FileDTO
            {
              Id = y.File.Id,
              Name = y.File.Name,
              ContentType = y.File.ContentType,
              Data = y.File.Data,
              Size = y.File.Size
            })
            .ToList()
        })
        .ToListAsync();
      return sidebarContent;
    }

    public async Task<List<UserCourseDetailsDTO>> HandleAsync(UserCourseQuery query)
    {
      var users = await _context.User
        .Include(t => t.Subscription)
        .ThenInclude(t => t.Course)
        .Include(t => t.ImageFile)
        .Where(query.Specification.Predicate)
        .Select(t => new UserCourseDetailsDTO
        {
          Id = t.Id,
          Name = t.Name,
          Surname = t.Surname,
          Username = t.Username,
          Email = t.Email,
          Created = t.Created,
          Picture = t.ImageFile != null ? Convert.ToBase64String(t.ImageFile.Data) : null,
          Admin = t.Id == t.Subscription.FirstOrDefault(x => x.CourseId == query.Id).Course.MadeById
        })
        .ToListAsync();
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

    public async Task<List<DiscussionDTO>> HandleAsync(CourseDiscussionsQuery query)
    {
      var courses = await _context.Discussion
        .Include(t => t.SubmittedBy)
        .ThenInclude(t => t.ImageFile)
        .Where(t => t.CourseId == query.CourseId)
        .Select(t => new DiscussionDTO() 
        {
          BackgroundColor = t.BackgroundColor,
          Content = t.Content,
          CourseId = t.CourseId,
          SubmittedAt = t.SubmittedAt,
          SubmittedById = t.SubmittedById,
          Id = t.Id,
          TextColor = t.TextColor,
          UserPictureBase64String = t.SubmittedBy.ImageFile != null ? Convert.ToBase64String(t.SubmittedBy.ImageFile.Data) : null,
          SubmittedBy = $"{t.SubmittedBy.Name} {t.SubmittedBy.Surname}"
        })
        .ToListAsync();
      return courses;
    }

    public async Task<int> HandleAsync(CourseTotalQuery query)
    {
      var count = await _context.Course.Where(query.Specification.Predicate).CountAsync();
      return count;
    }

    public async Task<int> HandleAsync(UserCourseTotalQuery query)
    {
      var count = await _context.User
        .Include(t => t.Subscription)
        .ThenInclude(t => t.Course)
        .Include(t => t.ImageFile)
        .Where(query.Specification.Predicate)
        .CountAsync();
      return count;
    }

    public async Task<List<NotificationQueryModel>> HandleAsync(CourseNotificationsQuery query)
    {
      var courses = _context.Notification
        .Include(t => t.NotificationFile)
        .Where(query.Specification.Predicate)
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
          CourseId = t.CourseId,
          SubmittedById = t.SubmittedById,
          Attachments = t.NotificationFile.Select(x => new FileDTO()
          {
            ContentType = x.File.ContentType,
            Data = x.File.Data,
            Name = x.File.Name,
            Id = x.File.Id,
            Size = x.File.Size
          }).ToList()
        });

      if (query.SortByNew)
      {
        courses = courses.OrderByDescending(t => t.SubmittedAt);
      }
      
      return await courses.ToListAsync();
    }
  }
}