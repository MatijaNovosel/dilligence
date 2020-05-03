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
    public virtual DbSet<Exam> Exam { get; set; }
    public virtual DbSet<ExamAttempt> ExamAttempt { get; set; }
    public virtual DbSet<File> File { get; set; }
    public virtual DbSet<Message> Message { get; set; }
    public virtual DbSet<Notification> Notification { get; set; }
    public virtual DbSet<NotificationUserSeen> NotificationUserSeen { get; set; }
    public virtual DbSet<Privileges> Privileges { get; set; }
    public virtual DbSet<Question> Question { get; set; }
    public virtual DbSet<QuestionType> QuestionType { get; set; }
    public virtual DbSet<SidebarContent> SidebarContent { get; set; }
    public virtual DbSet<SidebarContentFile> SidebarContentFile { get; set; }
    public virtual DbSet<Specialization> Specialization { get; set; }
    public virtual DbSet<Subscription> Subscription { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<UserAnswer> UserAnswer { get; set; }
    public virtual DbSet<UserNotificationBlacklist> UserNotificationBlacklist { get; set; }
    public virtual DbSet<UserPrivileges> UserPrivileges { get; set; }
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
                  .HasConstraintName("FK__Answer__Question__59063A47");
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
                  .HasConstraintName("FK__Chat__FirstParti__619B8048");

        entity.HasOne(d => d.SecondParticipant)
                  .WithMany(p => p.ChatSecondParticipant)
                  .HasForeignKey(d => d.SecondParticipantId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Chat__SecondPart__628FA481");
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
                  .HasConstraintName("FK__Course__Speciali__59FA5E80");
      });

      modelBuilder.Entity<Exam>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");

        entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__Exam__CourseID__5070F446");

        entity.HasOne(d => d.CreatedBy)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.CreatedById)
                  .HasConstraintName("FK__Exam__CreatedByI__5165187F");
      });

      modelBuilder.Entity<ExamAttempt>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.ExamId).HasColumnName("ExamID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Exam)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.ExamId)
                  .HasConstraintName("FK__ExamAttem__ExamI__534D60F1");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__ExamAttem__UserI__52593CB8");
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
                  .HasConstraintName("FK__Message__ChatID__6477ECF3");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Message)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Message__UserID__656C112C");
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
                  .HasConstraintName("FK__Notificat__Cours__5DCAEF64");

        entity.HasOne(d => d.SubmittedBy)
                  .WithMany(p => p.Notification)
                  .HasForeignKey(d => d.SubmittedById)
                  .HasConstraintName("FK__Notificat__Submi__5EBF139D");
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
                  .HasConstraintName("FK__Notificat__Notif__6B24EA82");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.NotificationUserSeen)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__Notificat__UserI__6C190EBB");
      });

      modelBuilder.Entity<Privileges>(entity =>
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
                  .HasConstraintName("FK__Question__ExamID__571DF1D5");

        entity.HasOne(d => d.Type)
                  .WithMany(p => p.Question)
                  .HasForeignKey(d => d.TypeId)
                  .HasConstraintName("FK__Question__TypeID__5812160E");
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
                  .HasConstraintName("FK__SidebarCo__Cours__5CD6CB2B");
      });

      modelBuilder.Entity<SidebarContentFile>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.FileId).HasColumnName("FileID");

        entity.Property(e => e.SidebarContentId).HasColumnName("SidebarContentID");

        entity.HasOne(d => d.File)
                  .WithMany(p => p.SidebarContentFile)
                  .HasForeignKey(d => d.FileId)
                  .HasConstraintName("FK__SidebarCo__FileI__60A75C0F");

        entity.HasOne(d => d.SidebarContent)
                  .WithMany(p => p.SidebarContentFile)
                  .HasForeignKey(d => d.SidebarContentId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK__SidebarCo__Sideb__5FB337D6");
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
                  .HasConstraintName("FK__Subscript__Cours__5AEE82B9");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Subscription)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__Subscript__UserI__5BE2A6F2");
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
                  .HasConstraintName("FK__User__ImageFileI__6383C8BA");
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
                  .HasConstraintName("FK__UserAnswe__Answe__5629CD9C");

        entity.HasOne(d => d.Attempt)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.AttemptId)
                  .HasConstraintName("FK__UserAnswe__Attem__5441852A");

        entity.HasOne(d => d.Question)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.QuestionId)
                  .HasConstraintName("FK__UserAnswe__Quest__5535A963");
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
                  .HasConstraintName("FK__UserNotif__Cours__6A30C649");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserNotificationBlacklist)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserNotif__UserI__693CA210");
      });

      modelBuilder.Entity<UserPrivileges>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.PrivilegeId).HasColumnName("PrivilegeID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Privilege)
                  .WithMany(p => p.UserPrivileges)
                  .HasForeignKey(d => d.PrivilegeId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserPrivi__Privi__6754599E");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserPrivileges)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserPrivi__UserI__66603565");
      });

      modelBuilder.Entity<UserSettings>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Locale)
                  .IsRequired()
                  .IsUnicode(false)
                  .HasDefaultValueSql("('en')");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserSettings)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserSetti__UserI__68487DD7");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
