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
using tvz2api_cqrs.Enumerations;

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
    IQueryHandlerAsync<CourseDiscussionsQuery, List<DiscussionDTO>>,
    IQueryHandlerAsync<CourseLandingPageQuery, string>,
    IQueryHandlerAsync<CourseDiscussionRepliesQuery, List<DiscussionReplyDTO>>,
    IQueryHandlerAsync<CourseUserGradesQuery, CourseUserGradesQueryModel>
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
          Specialization = t.Specialization.Name,
          Password = t.Password
        })
        .FirstOrDefaultAsync();
      return course;
    }

    public async Task<CourseUserGradesQueryModel> HandleAsync(CourseUserGradesQuery query)
    {
      var grades = new CourseUserGradesQueryModel();
      grades.Tasks = await _context
        .CourseTaskAttempt
        .Include(t => t.CourseTask)
        .Where(t => t.UserId == query.UserId && t.CourseTask.CourseId == query.CourseId)
        .Select(t => new CourseGradesTaskDTO()
        {
          Grade = t.Grade,
          MaximumGrade = t.CourseTask.GradeMaximum,
          Name = t.CourseTask.Title
        }).ToListAsync();

      var examAttempts = await _context
        .ExamAttempt
        .Include(t => t.Exam)
        .ThenInclude(t => t.Question)
        .ThenInclude(t => t.Answer)
        .Include(t => t.UserAnswer)
        .Where(t => t.UserId == query.UserId && t.Exam.CourseId == query.CourseId)
        .ToListAsync();

      grades.Exams = new List<CourseGradesExamDTO>();

      examAttempts.ForEach(x =>
      {
        double sumOfPoints = 0;
        int maximumPoints = 0;

        var questions = x.Exam.Question.ToList();
        var userAnswers = x.UserAnswer;

        questions.ForEach(x =>
        {
          maximumPoints += x.Value;

          if (x.TypeId == 1)
          {
            var userAnswer = userAnswers.FirstOrDefault(y => y.QuestionId == x.Id);

            if (userAnswer != null)
            {
              if (userAnswer.AnswerId != null)
              {
                if (x.Answer.Any(y => y.Correct == true && y.Id == userAnswer.AnswerId))
                {
                  sumOfPoints += x.Value;
                }
              }
            }
          }
          else
          {
            var userAnswer = userAnswers.Where(y => y.QuestionId == x.Id).ToList();
            if (userAnswer.Count != 0)
            {
              if (userAnswer.TrueForAll(y => y.AnswerId != null))
              {
                var answerIds = userAnswer.Select(y => y.AnswerId).ToList();
                var correctAnswerIds = x.Answer.Where(y => y.Correct == true).Select(y => y.Id).ToList();
                answerIds.ForEach(y =>
                {
                  if (correctAnswerIds.Contains((int)y))
                  {
                    sumOfPoints += Math.Ceiling((double)x.Value / correctAnswerIds.Count);
                  }
                });
              }
            }
          }
        });

        grades.Exams.Add(new CourseGradesExamDTO()
        {
          Grade = sumOfPoints,
          MaximumGrade = maximumPoints,
          Name = x.Exam.Name
        });
      });

      return grades;
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

    public async Task<string> HandleAsync(CourseLandingPageQuery query)
    {
      var course = await _context.Course.FirstOrDefaultAsync(x => x.Id == query.CourseId);
      return course.LandingPage;
    }

    public async Task<List<DiscussionReplyDTO>> HandleAsync(CourseDiscussionRepliesQuery query)
    {
      var replies = await _context.DiscussionComment
        .Include(t => t.SubmittedBy)
        .ThenInclude(t => t.ImageFile)
        .Where(t => t.DiscussionId == query.DiscussionId)
        .Select(t => new DiscussionReplyDTO()
        {
          Content = t.Content,
          Id = t.Id,
          SubmittedAt = t.SubmittedAt,
          SubmittedBy = $"{t.SubmittedBy.Name} {t.SubmittedBy.Surname}",
          SubmittedById = (int)t.SubmittedById,
          UserPicture = t.SubmittedBy.ImageFile != null ? Convert.ToBase64String(t.SubmittedBy.ImageFile.Data) : null,
        })
        .ToListAsync();
      return replies;
    }

    public async Task<List<UserCourseDetailsDTO>> HandleAsync(UserCourseQuery query)
    {
      var users = await _context.User
        .Include(t => t.Subscription)
        .ThenInclude(t => t.Course)
        .Include(t => t.ImageFile)
        .Include(t => t.UserCoursePrivilege)
        .Where(query.Specification.Predicate)
        .Select(t => new UserCourseDetailsDTO
        {
          Id = t.Id,
          Name = t.Name,
          Surname = t.Surname,
          Username = t.Username,
          Email = t.Email,
          Created = t.Created,
          Muted = !t.UserCoursePrivilege.Any(x => x.CourseId == query.Id && x.PrivilegeId == (int)PrivilegeEnum.CanCreateNewDiscussion),
          Picture = t.ImageFile != null ? Convert.ToBase64String(t.ImageFile.Data) : null,
          Admin = t.Id == t.Subscription.FirstOrDefault(x => x.CourseId == query.Id).Course.MadeById || t.UserCoursePrivilege.Any(x => x.CourseId == query.Id && x.PrivilegeId == (int)PrivilegeEnum.CanManageCourse)
        })
        .ToListAsync();
      return users;
    }

    public async Task<List<CourseQueryModel>> HandleAsync(CourseQuery query)
    {
      var courses = await _context
        .Course
        .Include(t => t.UserCoursePrivilege)
        .Where(query.Specification.Predicate)
        .Select(t => new CourseQueryModel
        {
          Id = t.Id,
          Name = t.Name,
          Ects = t.Ects,
          Subscribed = t.Subscription.Any(x => x.UserId == query.Specification.UserId),
          SpecializationId = t.SpecializationId,
          IsInvolved = t.UserCoursePrivilege.Any(x => x.UserId == query.Specification.UserId && x.PrivilegeId == (int)PrivilegeEnum.IsInvolvedWithCourse)
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
          Content = t.Content,
          CourseId = t.CourseId,
          SubmittedAt = t.SubmittedAt,
          SubmittedById = t.SubmittedById,
          Id = t.Id,
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