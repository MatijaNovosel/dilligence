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
    public virtual DbSet<Exam> Exam { get; set; }
    public virtual DbSet<ExamAttempt> ExamAttempt { get; set; }
    public virtual DbSet<File> File { get; set; }
    public virtual DbSet<Izvrsitelj> Izvrsitelj { get; set; }
    public virtual DbSet<Kolegij> Kolegij { get; set; }
    public virtual DbSet<Korisnik> Korisnik { get; set; }
    public virtual DbSet<Message> Message { get; set; }
    public virtual DbSet<Odjel> Odjel { get; set; }
    public virtual DbSet<Pretplata> Pretplata { get; set; }
    public virtual DbSet<Privileges> Privileges { get; set; }
    public virtual DbSet<Question> Question { get; set; }
    public virtual DbSet<QuestionType> QuestionType { get; set; }
    public virtual DbSet<SidebarContent> SidebarContent { get; set; }
    public virtual DbSet<SidebarContentFile> SidebarContentFile { get; set; }
    public virtual DbSet<Smjer> Smjer { get; set; }
    public virtual DbSet<Student> Student { get; set; }
    public virtual DbSet<StudentKolegij> StudentKolegij { get; set; }
    public virtual DbSet<UlogaIzvrsitelja> UlogaIzvrsitelja { get; set; }
    public virtual DbSet<UserAnswer> UserAnswer { get; set; }
    public virtual DbSet<UserPrivileges> UserPrivileges { get; set; }
    public virtual DbSet<UserSettings> UserSettings { get; set; }
    public virtual DbSet<Vijest> Vijest { get; set; }
    public virtual DbSet<VrstaZaposljenja> VrstaZaposljenja { get; set; }
    public virtual DbSet<Zaposlenik> Zaposlenik { get; set; }

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
                  .HasConstraintName("FK__Answer__Question__60A75C0F");
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

      modelBuilder.Entity<Exam>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

        entity.HasOne(d => d.CreatedBy)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.CreatedById)
                  .HasConstraintName("FK__Exam__CreatedByI__59063A47");

        entity.HasOne(d => d.Subject)
                  .WithMany(p => p.Exam)
                  .HasForeignKey(d => d.SubjectId)
                  .HasConstraintName("FK__Exam__SubjectID__5812160E");
      });

      modelBuilder.Entity<ExamAttempt>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.ExamId).HasColumnName("ExamID");

        entity.Property(e => e.UserId).HasColumnName("UserID");

        entity.HasOne(d => d.Exam)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.ExamId)
                  .HasConstraintName("FK__ExamAttem__ExamI__5AEE82B9");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.ExamAttempt)
                  .HasForeignKey(d => d.UserId)
                  .HasConstraintName("FK__ExamAttem__UserI__59FA5E80");
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

      modelBuilder.Entity<Izvrsitelj>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

        entity.Property(e => e.UlogaIzvrsiteljaId).HasColumnName("UlogaIzvrsiteljaID");

        entity.Property(e => e.ZaposlenikId).HasColumnName("ZaposlenikID");

        entity.HasOne(d => d.Kolegij)
                  .WithMany(p => p.Izvrsitelj)
                  .HasForeignKey(d => d.KolegijId)
                  .HasConstraintName("FK__Izvrsitel__Koleg__628FA481");

        entity.HasOne(d => d.UlogaIzvrsitelja)
                  .WithMany(p => p.Izvrsitelj)
                  .HasForeignKey(d => d.UlogaIzvrsiteljaId)
                  .HasConstraintName("FK__Izvrsitel__Uloga__6383C8BA");

        entity.HasOne(d => d.Zaposlenik)
                  .WithMany(p => p.Izvrsitelj)
                  .HasForeignKey(d => d.ZaposlenikId)
                  .HasConstraintName("FK__Izvrsitel__Zapos__6477ECF3");
      });

      modelBuilder.Entity<Kolegij>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Ects).HasColumnName("ECTS");

        entity.Property(e => e.Isvu)
                  .HasColumnName("ISVU")
                  .HasMaxLength(50)
                  .IsUnicode(false);

        entity.Property(e => e.IzradioId).HasColumnName("IzradioID");

        entity.Property(e => e.Lozinka)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.SmjerId).HasColumnName("SmjerID");

        entity.HasOne(d => d.Smjer)
                  .WithMany(p => p.Kolegij)
                  .HasForeignKey(d => d.SmjerId)
                  .HasConstraintName("FK__Kolegij__SmjerID__656C112C");
      });

      modelBuilder.Entity<Korisnik>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

        entity.Property(e => e.PasswordHash).HasMaxLength(255);

        entity.Property(e => e.PasswordSalt).HasMaxLength(255);

        entity.Property(e => e.Username)
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
                  .HasConstraintName("FK__Message__ChatID__73BA3083");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.Message)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__Message__UserID__74AE54BC");
      });

      modelBuilder.Entity<Odjel>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<Pretplata>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

        entity.Property(e => e.StudentId).HasColumnName("StudentID");

        entity.HasOne(d => d.Kolegij)
                  .WithMany(p => p.Pretplata)
                  .HasForeignKey(d => d.KolegijId)
                  .HasConstraintName("FK__Pretplata__Koleg__66603565");

        entity.HasOne(d => d.Student)
                  .WithMany(p => p.Pretplata)
                  .HasForeignKey(d => d.StudentId)
                  .HasConstraintName("FK__Pretplata__Stude__6754599E");
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
                  .HasConstraintName("FK__Question__ExamID__5EBF139D");

        entity.HasOne(d => d.Type)
                  .WithMany(p => p.Question)
                  .HasForeignKey(d => d.TypeId)
                  .HasConstraintName("FK__Question__TypeID__5FB337D6");
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

        entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

        entity.Property(e => e.Naslov)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Kolegij)
                  .WithMany(p => p.SidebarContent)
                  .HasForeignKey(d => d.KolegijId)
                  .HasConstraintName("FK__SidebarCo__Koleg__68487DD7");
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

      modelBuilder.Entity<Smjer>(entity =>
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

      modelBuilder.Entity<Student>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Email)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.ImagePath)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Ime)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Jmbag)
                  .HasColumnName("JMBAG")
                  .HasMaxLength(10)
                  .IsUnicode(false)
                  .IsFixedLength();

        entity.Property(e => e.Prezime)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.SmjerId).HasColumnName("SmjerID");

        entity.HasOne(d => d.Smjer)
                  .WithMany(p => p.Student)
                  .HasForeignKey(d => d.SmjerId)
                  .HasConstraintName("FK__Student__SmjerID__693CA210");
      });

      modelBuilder.Entity<StudentKolegij>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

        entity.Property(e => e.StudentId).HasColumnName("StudentID");

        entity.HasOne(d => d.Kolegij)
                  .WithMany(p => p.StudentKolegij)
                  .HasForeignKey(d => d.KolegijId)
                  .HasConstraintName("FK__StudentKo__Koleg__6A30C649");

        entity.HasOne(d => d.Student)
                  .WithMany(p => p.StudentKolegij)
                  .HasForeignKey(d => d.StudentId)
                  .HasConstraintName("FK__StudentKo__Stude__6B24EA82");
      });

      modelBuilder.Entity<UlogaIzvrsitelja>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);
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
                  .HasConstraintName("FK__UserAnswe__Answe__5DCAEF64");

        entity.HasOne(d => d.Attempt)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.AttemptId)
                  .HasConstraintName("FK__UserAnswe__Attem__5BE2A6F2");

        entity.HasOne(d => d.Question)
                  .WithMany(p => p.UserAnswer)
                  .HasForeignKey(d => d.QuestionId)
                  .HasConstraintName("FK__UserAnswe__Quest__5CD6CB2B");
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
                  .HasConstraintName("FK__UserPrivi__Privi__76969D2E");

        entity.HasOne(d => d.User)
                  .WithMany(p => p.UserPrivileges)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__UserPrivi__UserI__75A278F5");
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
                  .HasConstraintName("FK__UserSetti__UserI__778AC167");
      });

      modelBuilder.Entity<Vijest>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Datum).HasColumnType("date");

        entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

        entity.Property(e => e.Naslov)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.ObjavioId).HasColumnName("ObjavioID");

        entity.Property(e => e.Opis)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.HasOne(d => d.Kolegij)
                  .WithMany(p => p.Vijest)
                  .HasForeignKey(d => d.KolegijId)
                  .HasConstraintName("FK__Vijest__KolegijI__6C190EBB");

        entity.HasOne(d => d.Objavio)
                  .WithMany(p => p.Vijest)
                  .HasForeignKey(d => d.ObjavioId)
                  .HasConstraintName("FK__Vijest__ObjavioI__6D0D32F4");
      });

      modelBuilder.Entity<VrstaZaposljenja>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Naziv)
                  .HasMaxLength(255)
                  .IsUnicode(false);
      });

      modelBuilder.Entity<Zaposlenik>(entity =>
      {
        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Email)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.ImagePath)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.Ime)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.OdjelId).HasColumnName("OdjelID");

        entity.Property(e => e.Prezime)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.TitulaIspred)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.TitulaIza)
                  .HasMaxLength(255)
                  .IsUnicode(false);

        entity.Property(e => e.VrstaZaposljenjaId).HasColumnName("VrstaZaposljenjaID");

        entity.HasOne(d => d.Odjel)
                  .WithMany(p => p.Zaposlenik)
                  .HasForeignKey(d => d.OdjelId)
                  .HasConstraintName("FK__Zaposleni__Odjel__6E01572D");

        entity.HasOne(d => d.VrstaZaposljenja)
                  .WithMany(p => p.Zaposlenik)
                  .HasForeignKey(d => d.VrstaZaposljenjaId)
                  .HasConstraintName("FK__Zaposleni__Vrsta__6EF57B66");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
