export default {
  // Global privileges
  CanGrantPrivileges: 1, // Supreme admin role, can give other users privileges - has access to admin console view
  CanViewGlobalUsers: 2, // Has access to a view with all users
  CanCreateCourse: 3, // Can create courses, the user automatically gains access to the CanManageCourse privilege when creating a new one
  // Course specific
  CanManageCourse: 4, // Overarching privilege, has access to all other privileges related to the course being created
  // Course specific -> Course landing page
  CanChangeCourseLandingPage: 5, // Can change course landing page contents
  // Course specific -> Course notifications
  CanManageNotifications: 6, // Overarching privilege for notifications, has access to all related privileges regarding notificiations
  CanSendNotifications: 7, // Can send notifications to users subscribed to the course
  CanDeleteNotifications: 8, // Can delete said notifications, even ones not made by the sender
  CanArchiveNotifications: 9, // Can archive notifications, even ones not made by the sender
  // Course specific -> Course participants
  CanKickParticipants: 10, // Can remove users from the course, removes subscriptions as well
  CanMuteParticipants: 11, // Can mute participants, disabling their ability to participate in discussions on the course
  // Course specific -> Course tasks
  CanManageTasks: 12, // Overarching privilege, has access to all other privileges related to course tasks
  CanCreateTasks: 13, // Can create tasks
  CanDeleteTasks: 14, // Can delete tasks, even those made by others
  CanGradeTasks: 15, // Can grade tasks, even those made by others
  // Course specific -> Course exams
  CanManageExams: 16, // Overarching privilege, has access to all other privileges related to the course exams
  CanCreateExams: 17, // Can create exams
  CanDeleteExams: 18, // Can delete exams, even those made by others
  CanGradeExams: 19, // Can grade exams, even those made by others,
  // Course specific -> Course discussion
  CanManageDiscussion: 20, // Overarching privilege, has access to all other privileges related to course discussion
  CanCreateNewDiscussion: 21, // Can create new discussion threads
  CanDeleteDiscussion: 22, // Can delete discussion thread, even those made by others
  // Course specific -> Course files
  CanManageCourseFiles: 23, // Overarching privilege, has access to all other privileges related to course files
  CanUploadCourseFiles: 24, // Can upload files to the course
  CanDeleteCourseFiles: 25, // Can delete files, even those uploaded by others
  // Course specific -> Roles
  IsInvolvedToCourse: 26 // If the user is involved with the course somehow, admin or other sub roles, used to differentiate users and admins
};