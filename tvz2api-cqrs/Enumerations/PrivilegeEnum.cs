using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tvz2api_cqrs.Enumerations
{
  public enum PrivilegeEnum
  {
    CanGrantPrivileges = 1,
    CanViewGlobalUsers = 2,
    CanCreateCourse = 3,
    CanManageCourse = 4,
    CanChangeCourseLandingPage = 5,
    CanManageNotifications = 6,
    CanSendNotifications = 7,
    CanDeleteNotifications = 8,
    CanArchiveNotifications = 9,
    CanKickParticipants = 10,
    CanMuteParticipants = 11,
    CanManageTasks = 12,
    CanCreateTasks = 13,
    CanDeleteTasks = 14,
    CanGradeTasks = 15,
    CanManageExams = 16,
    CanCreateExams = 17,
    CanDeleteExams = 18,
    CanGradeExams = 19,
    CanManageDiscussion = 20,
    CanCreateNewDiscussion = 21,
    CanDeleteDiscussion = 22,
    CanManageCourseFiles = 23,
    CanUploadCourseFiles = 24,
    CanDeleteCourseFiles = 25,
    IsInvolvedWithCourse = 26
  }
}
