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
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Izvrsitelj> Izvrsitelj { get; set; }
        public virtual DbSet<Kolegij> Kolegij { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Odjel> Odjel { get; set; }
        public virtual DbSet<Pretplata> Pretplata { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<SidebarContent> SidebarContent { get; set; }
        public virtual DbSet<SidebarContentFile> SidebarContentFile { get; set; }
        public virtual DbSet<Smjer> Smjer { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentKolegij> StudentKolegij { get; set; }
        public virtual DbSet<UlogaIzvrsitelja> UlogaIzvrsitelja { get; set; }
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
                    .HasConstraintName("FK__Answer__Question__5DCAEF64");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Score).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Exam__SubjectID__5AEE82B9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Exam__UserID__59FA5E80");
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
                    .HasConstraintName("FK__Izvrsitel__Koleg__5FB337D6");

                entity.HasOne(d => d.UlogaIzvrsitelja)
                    .WithMany(p => p.Izvrsitelj)
                    .HasForeignKey(d => d.UlogaIzvrsiteljaId)
                    .HasConstraintName("FK__Izvrsitel__Uloga__60A75C0F");

                entity.HasOne(d => d.Zaposlenik)
                    .WithMany(p => p.Izvrsitelj)
                    .HasForeignKey(d => d.ZaposlenikId)
                    .HasConstraintName("FK__Izvrsitel__Zapos__619B8048");
            });

            modelBuilder.Entity<Kolegij>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AkademskaGodina)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cilj)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ects).HasColumnName("ECTS");

                entity.Property(e => e.Ishodi)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Isvu)
                    .HasColumnName("ISVU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isvuekvivalencije)
                    .HasColumnName("ISVUEkvivalencije")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IzradioId).HasColumnName("IzradioID");

                entity.Property(e => e.IzvedbaNastave)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Literatura)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MaterijalniUvjeti)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NacinIzvodenjaAuditornih)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NacinIzvodenjaLaboratorijskih)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NacinIzvodenjaPredavanja)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NaciniPolaganja)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Napomena)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Naziv)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PracenjeRada)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Preduvjeti)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProvjeraZnanja)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SadrzajAuditornih)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SadrzajLaboratorijskih)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SadrzajPredavanja)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SmjerId).HasColumnName("SmjerID");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Uvjet)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Smjer)
                    .WithMany(p => p.Kolegij)
                    .HasForeignKey(d => d.SmjerId)
                    .HasConstraintName("FK__Kolegij__SmjerID__628FA481");
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.PasswordSalt).HasMaxLength(255);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);
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
                    .HasConstraintName("FK__Pretplata__Koleg__6383C8BA");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Pretplata)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Pretplata__Stude__6477ECF3");
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
                    .HasConstraintName("FK__Question__ExamID__5BE2A6F2");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Question__TypeID__5CD6CB2B");
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
                    .HasConstraintName("FK__SidebarCo__Koleg__656C112C");
            });

            modelBuilder.Entity<SidebarContentFile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.SidebarContentId).HasColumnName("SidebarContentID");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.SidebarContentFile)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK__SidebarCo__FileI__6E01572D");

                entity.HasOne(d => d.SidebarContent)
                    .WithMany(p => p.SidebarContentFile)
                    .HasForeignKey(d => d.SidebarContentId)
                    .HasConstraintName("FK__SidebarCo__Sideb__6D0D32F4");
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
                    .HasConstraintName("FK__Student__SmjerID__66603565");
            });

            modelBuilder.Entity<StudentKolegij>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Kolegij)
                    .WithMany(p => p.StudentKolegij)
                    .HasForeignKey(d => d.KolegijId)
                    .HasConstraintName("FK__StudentKo__Koleg__6754599E");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentKolegij)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__StudentKo__Stude__68487DD7");
            });

            modelBuilder.Entity<UlogaIzvrsitelja>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(255)
                    .IsUnicode(false);
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
                    .HasConstraintName("FK__Vijest__KolegijI__693CA210");

                entity.HasOne(d => d.Objavio)
                    .WithMany(p => p.Vijest)
                    .HasForeignKey(d => d.ObjavioId)
                    .HasConstraintName("FK__Vijest__ObjavioI__6A30C649");
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
                    .HasConstraintName("FK__Zaposleni__Odjel__6B24EA82");

                entity.HasOne(d => d.VrstaZaposljenja)
                    .WithMany(p => p.Zaposlenik)
                    .HasForeignKey(d => d.VrstaZaposljenjaId)
                    .HasConstraintName("FK__Zaposleni__Vrsta__6C190EBB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
