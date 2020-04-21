using System.Collections.Generic;
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
  public class CourseQueryHandler :
    IQueryHandlerAsync<CourseQuery, List<CourseQueryModel>>,
    IQueryHandlerAsync<CourseTotalQuery, int>,
    IQueryHandlerAsync<UserCourseTotalQuery, int>,
    IQueryHandlerAsync<UserCourseQuery, List<UserDTO>>,
    IQueryHandlerAsync<CourseDetailsQuery, CourseDetailsQueryModel>,
    IQueryHandlerAsync<CourseSidebarQuery, List<SidebarContentDTO>>
  {
    private readonly tvz2Context _context;

    public CourseQueryHandler(tvz2Context context)
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
          Naziv = t.Name,
          Ects = t.Ects,
          Smjer = t.Specialization.Name,
          Users = t.Subscription
            .Select(x => new UserDTO
            {
              Id = x.User.Id,
              Name = x.User.Name,
              Surname = x.User.Surname,
              ImageFileId = x.User.ImageFileId,
              Email = x.User.Email
            })
            .ToList(),
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
                  Naziv = y.File.Name,
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
              Naziv = y.File.Name,
              ContentType = y.File.ContentType,
              Data = y.File.Data
            })
            .ToList()
        })
        .ToListAsync();
      return sidebarContent;
    }

    public async Task<List<UserDTO>> HandleAsync(UserCourseQuery query)
    {
      var users = await _context.User
        .Where(t => t.Subscription.Any(x => x.CourseId == query.Id))
        .Select(t => new UserDTO
        {
          Id = t.Id,
          Name = t.Name,
          Surname = t.Surname,
          Email = t.Email
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
          Naziv = t.Name,
          Ects = t.Ects,
          SmjerId = t.SpecializationId
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
          Naziv = t.Name,
          Ects = t.Ects,
          SmjerId = t.SpecializationId
        }).CountAsync();
      return count;
    }

    public async Task<int> HandleAsync(UserCourseTotalQuery query)
    {
      var count = await _context.User
        .Where(t => t.Subscription.Any(x => x.CourseId == query.Id))
        .Select(t => new UserDTO
        {
          Id = t.Id,
          Name = t.Name,
          Surname = t.Surname,
          Email = t.Email
        })
        .CountAsync();
      return count;
    }
  }
}