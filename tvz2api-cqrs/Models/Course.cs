﻿using System;
using System.Collections.Generic;

namespace tvz2api_cqrs.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseTask = new HashSet<CourseTask>();
            Exam = new HashSet<Exam>();
            Notification = new HashSet<Notification>();
            SidebarContent = new HashSet<SidebarContent>();
            Subscription = new HashSet<Subscription>();
            UserCoursePrivilege = new HashSet<UserCoursePrivilege>();
            UserNotificationBlacklist = new HashSet<UserNotificationBlacklist>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Isvu { get; set; }
        public int? Ects { get; set; }
        public string Password { get; set; }
        public int? MadeById { get; set; }
        public int? SpecializationId { get; set; }

        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<CourseTask> CourseTask { get; set; }
        public virtual ICollection<Exam> Exam { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<SidebarContent> SidebarContent { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
        public virtual ICollection<UserCoursePrivilege> UserCoursePrivilege { get; set; }
        public virtual ICollection<UserNotificationBlacklist> UserNotificationBlacklist { get; set; }
    }
}
