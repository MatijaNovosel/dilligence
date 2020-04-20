using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tvz2api_cqrs.Models
{
  public partial class tvz2Context : DbContext
  {
    public tvz2Context()
    {
    }

    public tvz2Context(DbContextOptions<tvz2Context> options)
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
    public virtual DbSet<Privileges> Privileges { get; set; }
    public virtual DbSet<Question> Question { get; set; }
    public virtual DbSet<QuestionType> QuestionType { get; set; }
    public virtual DbSet<SidebarContent> SidebarContent { get; set; }
    public virtual DbSet<SidebarContentFile> SidebarContentFile { get; set; }
    public virtual DbSet<Specialization> Specialization { get; set; }
    public virtual DbSet<Subscription> Subscription { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<UserAnswer> UserAnswer { get; set; }
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
                  .HasConstraintName("FK__Answer__Question__534D60F1");
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
                  .HasConstraintName("FK__Chat__FirstParti__5CD6CB2B");

        entity.HasOne(d => d.SecondParticipant)
                  .WithMany(p => p.ChatSecondParticipant)
                  .HasForeignKey(d => d.SecondParticipantId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Chat__SecondPart__5DCAEF64");
      });

      modelBuilder.Entity<Course>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Ects).HasColumnName("ECTS");

        entity.Property(e => e.Isvu)
                  .HasColumnName("ISVU")
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.Lozinka)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.MadeById).HasColumnName("MadeByID");

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.SpecializationId).HasColumnName("SpecializationID");

        entity.HasOne(d => d.Specialization)
                  .WithMany(p => p.Course)
                  .HasForeignKey(d => d.SpecializationId)
                  .HasConstraintName("FK__Course__Speciali__5535A963");
      });

      modelBuilder.Entity<Exam>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__Exam__CourseID__4AB81AF0");

        entity.HasOne(d => d.CreatedBy)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.CreatedById)
                  .HasConstraintName("FK__Exam__CreatedByI__4BAC3F29");
      });

      modelBuilder.Entity<ExamAttempt>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.ExamId).HasColumnName("ExamID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Exam)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.ExamId)
                  .HasConstraintName("FK__ExamAttem__ExamI__4D94879B");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__ExamAttem__UserI__4CA06362");
      });

      modelBuilder.Entity<File>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.ContentType)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Naziv)
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
                  .HasConstraintName("FK__Message__ChatID__5FB337D6");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Message)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Message__UserID__60A75C0F");
      });

      modelBuilder.Entity<Notification>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.Datum).HasColumnType("date");

        entity.Property(e => e.Naslov)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Opis)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.SubmittedById).HasColumnName("SubmittedByID");

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.Notification)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__Notificat__Cours__59063A47");

        entity.HasOne(d => d.SubmittedBy)
                  .WithMany(p => p.Notification)
                  .HasForeignKey(d => d.SubmittedById)
                  .HasConstraintName("FK__Notificat__Submi__59FA5E80");
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
                  .HasConstraintName("FK__Question__ExamID__5165187F");

        entity.HasOne(d => d.Type)
                  .WithMany(p => p.Question)
                  .HasForeignKey(d => d.TypeId)
                  .HasConstraintName("FK__Question__TypeID__52593CB8");
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

        entity.Property(e => e.Naslov)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.SidebarContent)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__SidebarCo__Cours__5812160E");
      });

      modelBuilder.Entity<SidebarContentFile>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.FileId).HasColumnName("FileID");

        entity.Property(e => e.SidebarContentId).HasColumnName("SidebarContentID");

        entity.HasOne(d => d.File)
                  .WithMany(p => p.SidebarContentFile)
                  .HasForeignKey(d => d.FileId)
                  .HasConstraintName("FK__SidebarCo__FileI__5BE2A6F2");

        entity.HasOne(d => d.SidebarContent)
                  .WithMany(p => p.SidebarContentFile)
                  .HasForeignKey(d => d.SidebarContentId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK__SidebarCo__Sideb__5AEE82B9");
      });

      modelBuilder.Entity<Specialization>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.SkraceniNaziv)
                  .HasMaxLength(20)
                  .IsUnicode(false);

        entity.Property(e => e.Vanredno).HasDefaultValueSql("((0))");
      });

      modelBuilder.Entity<Subscription>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CourseId).HasColumnName("CourseID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Course)
                  .WithMany(p => p.Subscription)
                  .HasForeignKey(d => d.CourseId)
                  .HasConstraintName("FK__Subscript__Cours__5629CD9C");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Subscription)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__Subscript__UserI__571DF1D5");
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
                  .HasColumnName("surname")
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Username)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.ImageFile)
                  .WithMany(p => p.User)
                  .HasForeignKey(d => d.ImageFileId)
                  .HasConstraintName("FK__User__ImageFileI__5EBF139D");
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
                  .HasConstraintName("FK__UserAnswe__Answe__5070F446");

        entity.HasOne(d => d.Attempt)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.AttemptId)
                  .HasConstraintName("FK__UserAnswe__Attem__4E88ABD4");

        entity.HasOne(d => d.Question)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.QuestionId)
                  .HasConstraintName("FK__UserAnswe__Quest__4F7CD00D");
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
                  .HasConstraintName("FK__UserPrivi__Privi__628FA481");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserPrivileges)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserPrivi__UserI__619B8048");
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
                  .HasConstraintName("FK__UserSetti__UserI__6383C8BA");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
