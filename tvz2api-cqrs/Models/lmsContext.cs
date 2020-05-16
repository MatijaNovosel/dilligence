using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tvz2api_cqrs.Models
{
  public partial class lmsContext : DbContext
  {
    public lmsContext()
    {
    }

    public lmsContext(DbContextOptions<lmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answer { get; set; }
    public virtual DbSet<Chat> Chat { get; set; }
    public virtual DbSet<Course> Course { get; set; }
    public virtual DbSet<CourseTask> CourseTask { get; set; }
    public virtual DbSet<CourseTaskAttachment> CourseTaskAttachment { get; set; }
    public virtual DbSet<CourseTaskAttempt> CourseTaskAttempt { get; set; }
    public virtual DbSet<Exam> Exam { get; set; }
    public virtual DbSet<ExamAttempt> ExamAttempt { get; set; }
    public virtual DbSet<File> File { get; set; }
    public virtual DbSet<Message> Message { get; set; }
    public virtual DbSet<Notification> Notification { get; set; }
    public virtual DbSet<NotificationFile> NotificationFile { get; set; }
    public virtual DbSet<NotificationUserSeen> NotificationUserSeen { get; set; }
    public virtual DbSet<Privilege> Privilege { get; set; }
    public virtual DbSet<Question> Question { get; set; }
    public virtual DbSet<QuestionType> QuestionType { get; set; }
    public virtual DbSet<SidebarContent> SidebarContent { get; set; }
    public virtual DbSet<SidebarContentFile> SidebarContentFile { get; set; }
    public virtual DbSet<Specialization> Specialization { get; set; }
    public virtual DbSet<Subscription> Subscription { get; set; }
    public virtual DbSet<TaskAttemptAttachment> TaskAttemptAttachment { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<UserAnswer> UserAnswer { get; set; }
    public virtual DbSet<UserCoursePrivilege> UserCoursePrivilege { get; set; }
    public virtual DbSet<UserNotificationBlacklist> UserNotificationBlacklist { get; set; }
    public virtual DbSet<UserPrivilege> UserPrivilege { get; set; }
    public virtual DbSet<UserSettings> UserSettings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("Server=.;Database=tvz2;Trusted_Connection=True;");
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Answer>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Content).IsUnicode(false);

        entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

        entity.HasOne(d => d.Question)
                  .WithMany(p => p.Answer)
                  .HasForeignKey(d => d.QuestionId)
                  .HasConstraintName("FK__Answer__Question__693CA210");
      });

      modelBuilder.Entity<Chat>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.FirstParticipantId).HasColumnName("FirstParticipantID");

        entity.Property(e => e.SecondParticipantId).HasColumnName("SecondParticipantID");

        entity.HasOne(d => d.FirstParticipant)
                  .WithMany(p => p.ChatFirstParticipant)
                  .HasForeignKey(d => d.FirstParticipantId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Chat__FirstParti__71D1E811");

        entity.HasOne(d => d.SecondParticipant)
                  .WithMany(p => p.ChatSecondParticipant)
                  .HasForeignKey(d => d.SecondParticipantId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Chat__SecondPart__72C60C4A");
      });

      modelBuilder.Entity<Course>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Ects).HasColumnName("ECTS");

        entity.Property(e => e.Isvu)
                  .HasColumnName("ISVU")
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.MadeById).HasColumnName("MadeByID");

        entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Password)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

        entity.HasOne(d => d.Specialization)
                  .WithMany(p => p.Course)
                  .HasForeignKey(d => d.SpecializationId)
                  .HasConstraintName("FK__Course__Speciali__6A30C649");
      });

      modelBuilder.Entity<CourseTask>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");

        entity.Property(e => e.Description)
                  .IsRequired()
                  .IsUnicode(false);

        entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(getdate())");

        entity.Property(e => e.Title)
                  .IsRequired()
                  .IsUnicode(false);

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.CourseTask)
                  .HasForeignKey(d => d.CourseId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__CourseTas__Cours__06CD04F7");

        entity.HasOne(d => d.CreatedBy)
                  .WithMany(p => p.CourseTask)
                  .HasForeignKey(d => d.CreatedById)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__CourseTas__Creat__7F2BE32F");
      });

      modelBuilder.Entity<CourseTaskAttachment>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseTaskId).HasColumnName("CourseTaskID");

        entity.Property(e => e.FileId).HasColumnName("FileID");

        entity.HasOne(d => d.CourseTask)
                  .WithMany(p => p.CourseTaskAttachment)
                  .HasForeignKey(d => d.CourseTaskId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__CourseTas__Cours__01142BA1");

        entity.HasOne(d => d.File)
                  .WithMany(p => p.CourseTaskAttachment)
                  .HasForeignKey(d => d.FileId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__CourseTas__FileI__00200768");
      });

      modelBuilder.Entity<CourseTaskAttempt>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseTaskId).HasColumnName("CourseTaskID");

        entity.Property(e => e.Description)
                  .IsRequired()
                  .IsUnicode(false);

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.CourseTask)
                  .WithMany(p => p.CourseTaskAttempt)
                  .HasForeignKey(d => d.CourseTaskId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__CourseTas__Cours__02084FDA");

        entity.HasOne(d => d.GradedBy)
                  .WithMany(p => p.CourseTaskAttemptGradedBy)
                  .HasForeignKey(d => d.GradedById)
                  .HasConstraintName("FK__CourseTas__Grade__03F0984C");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.CourseTaskAttemptUser)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__CourseTas__UserI__02FC7413");
      });

      modelBuilder.Entity<Exam>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");

        entity.Property(e => e.Finalized).HasDefaultValueSql("((0))");

        entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__Exam__CourseID__60A75C0F");

        entity.HasOne(d => d.CreatedBy)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.CreatedById)
                  .HasConstraintName("FK__Exam__CreatedByI__619B8048");
      });

      modelBuilder.Entity<ExamAttempt>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.ExamId).HasColumnName("ExamID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Exam)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.ExamId)
                  .HasConstraintName("FK__ExamAttem__ExamI__6383C8BA");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__ExamAttem__UserI__628FA481");
      });

      modelBuilder.Entity<File>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.ContentType)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<Message>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.ChatId).HasColumnName("ChatID");

        entity.Property(e => e.Content)
                  .IsRequired()
                  .IsUnicode(false);

        entity.Property(e => e.SentAt).HasDefaultValueSql("(getdate())");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Chat)
                  .WithMany(p => p.Message)
                  .HasForeignKey(d => d.ChatId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Message__ChatID__74AE54BC");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Message)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Message__UserID__75A278F5");
      });

      modelBuilder.Entity<Notification>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Color)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.Description).IsUnicode(false);

        entity.Property(e => e.ExpiresAt).HasColumnType("date");

        entity.Property(e => e.SubmittedAt).HasColumnType("date");

        entity.Property(e => e.SubmittedById).HasColumnName("SubmittedByID");

        entity.Property(e => e.Title)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.Notification)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__Notificat__Cours__6E01572D");

        entity.HasOne(d => d.SubmittedBy)
                  .WithMany(p => p.Notification)
                  .HasForeignKey(d => d.SubmittedById)
                  .HasConstraintName("FK__Notificat__Submi__6EF57B66");
      });

      modelBuilder.Entity<NotificationFile>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.FileId).HasColumnName("FileID");

        entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

        entity.HasOne(d => d.File)
                  .WithMany(p => p.NotificationFile)
                  .HasForeignKey(d => d.FileId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Notificat__FileI__7E37BEF6");

        entity.HasOne(d => d.Notification)
                  .WithMany(p => p.NotificationFile)
                  .HasForeignKey(d => d.NotificationId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Notificat__Notif__7D439ABD");
      });

      modelBuilder.Entity<NotificationUserSeen>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.NotificationId).HasColumnName("NotificationID");

        entity.Property(e => e.Seen).HasDefaultValueSql("((0))");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Notification)
                  .WithMany(p => p.NotificationUserSeen)
                  .HasForeignKey(d => d.NotificationId)
                  .HasConstraintName("FK__Notificat__Notif__7B5B524B");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.NotificationUserSeen)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__Notificat__UserI__7C4F7684");
      });

      modelBuilder.Entity<Privilege>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Name)
                  .IsRequired()
                  .IsUnicode(false);
      });

      modelBuilder.Entity<Question>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Content).IsUnicode(false);

        entity.Property(e => e.ExamId).HasColumnName("ExamID");

        entity.Property(e => e.Title)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.TypeId).HasColumnName("TypeID");

        entity.HasOne(d => d.Exam)
                  .WithMany(p => p.Question)
                  .HasForeignKey(d => d.ExamId)
                  .HasConstraintName("FK__Question__ExamID__6754599E");

        entity.HasOne(d => d.Type)
                  .WithMany(p => p.Question)
                  .HasForeignKey(d => d.TypeId)
                  .HasConstraintName("FK__Question__TypeID__68487DD7");
      });

      modelBuilder.Entity<QuestionType>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<SidebarContent>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.Title)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.SidebarContent)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__SidebarCo__Cours__6D0D32F4");
      });

      modelBuilder.Entity<SidebarContentFile>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.FileId).HasColumnName("FileID");

        entity.Property(e => e.SidebarContentId).HasColumnName("SidebarContentID");

        entity.HasOne(d => d.File)
                  .WithMany(p => p.SidebarContentFile)
                  .HasForeignKey(d => d.FileId)
                  .HasConstraintName("FK__SidebarCo__FileI__70DDC3D8");

        entity.HasOne(d => d.SidebarContent)
                  .WithMany(p => p.SidebarContentFile)
                  .HasForeignKey(d => d.SidebarContentId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK__SidebarCo__Sideb__6FE99F9F");
      });

      modelBuilder.Entity<Specialization>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Shorthand)
                  .HasMaxLength(20)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<Subscription>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.JoinedAt).HasDefaultValueSql("(getdate())");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.Subscription)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__Subscript__Cours__6B24EA82");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Subscription)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__Subscript__UserI__6C190EBB");
      });

      modelBuilder.Entity<TaskAttemptAttachment>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseTaskAttemptId).HasColumnName("CourseTaskAttemptID");

        entity.Property(e => e.FileId).HasColumnName("FileID");

        entity.HasOne(d => d.CourseTaskAttempt)
                  .WithMany(p => p.TaskAttemptAttachment)
                  .HasForeignKey(d => d.CourseTaskAttemptId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__TaskAttem__Cours__04E4BC85");

        entity.HasOne(d => d.File)
                  .WithMany(p => p.TaskAttemptAttachment)
                  .HasForeignKey(d => d.FileId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__TaskAttem__FileI__05D8E0BE");
      });

      modelBuilder.Entity<User>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

        entity.Property(e => e.Email)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.ImageFileId).HasColumnName("ImageFileID");

        entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.PasswordHash).HasMaxLength(255);

        entity.Property(e => e.PasswordSalt).HasMaxLength(255);

        entity.Property(e => e.Surname)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Username)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.ImageFile)
                  .WithMany(p => p.User)
                  .HasForeignKey(d => d.ImageFileId)
                  .HasConstraintName("FK__User__ImageFileI__73BA3083");
      });

      modelBuilder.Entity<UserAnswer>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

        entity.Property(e => e.AttemptId).HasColumnName("AttemptID");

        entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

        entity.HasOne(d => d.Answer)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.AnswerId)
                  .HasConstraintName("FK__UserAnswe__Answe__66603565");

        entity.HasOne(d => d.Attempt)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.AttemptId)
                  .HasConstraintName("FK__UserAnswe__Attem__6477ECF3");

        entity.HasOne(d => d.Question)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.QuestionId)
                  .HasConstraintName("FK__UserAnswe__Quest__656C112C");
      });

      modelBuilder.Entity<UserCoursePrivilege>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.PrivilegeId).HasColumnName("PrivilegeID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.UserCoursePrivilege)
                  .HasForeignKey(d => d.CourseId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserCours__Cours__07C12930");

        entity.HasOne(d => d.Privilege)
                  .WithMany(p => p.UserCoursePrivilege)
                  .HasForeignKey(d => d.PrivilegeId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserCours__Privi__09A971A2");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserCoursePrivilege)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserCours__UserI__08B54D69");
      });

      modelBuilder.Entity<UserNotificationBlacklist>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.UserNotificationBlacklist)
                  .HasForeignKey(d => d.CourseId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserNotif__Cours__7A672E12");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserNotificationBlacklist)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserNotif__UserI__797309D9");
      });

      modelBuilder.Entity<UserPrivilege>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.PrivilegeId).HasColumnName("PrivilegeID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Privilege)
                  .WithMany(p => p.UserPrivilege)
                  .HasForeignKey(d => d.PrivilegeId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserPrivi__Privi__778AC167");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserPrivilege)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserPrivi__UserI__76969D2E");
      });

      modelBuilder.Entity<UserSettings>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.DarkMode).HasDefaultValueSql("((0))");

        entity.Property(e => e.Locale)
                  .IsRequired()
                  .HasMaxLength(2)
                  .IsUnicode(false)
                  .HasDefaultValueSql("('en')");

        entity.Property(e => e.Popups).HasDefaultValueSql("((1))");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserSettings)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserSetti__UserI__787EE5A0");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
