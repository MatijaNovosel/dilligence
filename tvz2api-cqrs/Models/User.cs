using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class User
    {
        public User()
        {
            ChatFirstParticipant = new HashSet<Chat>();
            ChatSecondParticipant = new HashSet<Chat>();
            CourseTask = new HashSet<CourseTask>();
            CourseTaskAttemptGradedBy = new HashSet<CourseTaskAttempt>();
            CourseTaskAttemptUser = new HashSet<CourseTaskAttempt>();
            Exam = new HashSet<Exam>();
            ExamAttempt = new HashSet<ExamAttempt>();
            Message = new HashSet<Message>();
            Notification = new HashSet<Notification>();
            NotificationUserSeen = new HashSet<NotificationUserSeen>();
            Subscription = new HashSet<Subscription>();
            UserNotificationBlacklist = new HashSet<UserNotificationBlacklist>();
            UserPrivilege = new HashSet<UserPrivilege>();
            UserSettings = new HashSet<UserSettings>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime? Created { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? ImageFileId { get; set; }

        public virtual File ImageFile { get; set; }
        public virtual ICollection<Chat> ChatFirstParticipant { get; set; }
        public virtual ICollection<Chat> ChatSecondParticipant { get; set; }
        public virtual ICollection<CourseTask> CourseTask { get; set; }
        public virtual ICollection<CourseTaskAttempt> CourseTaskAttemptGradedBy { get; set; }
        public virtual ICollection<CourseTaskAttempt> CourseTaskAttemptUser { get; set; }
        public virtual ICollection<Exam> Exam { get; set; }
        public virtual ICollection<ExamAttempt> ExamAttempt { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<NotificationUserSeen> NotificationUserSeen { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual ICollection<UserNotificationBlacklist> UserNotificationBlacklist { get; set; }
        public virtual ICollection<UserPrivilege> UserPrivilege { get; set; }
        public virtual ICollection<UserSettings> UserSettings { get; set; }
    }
}
