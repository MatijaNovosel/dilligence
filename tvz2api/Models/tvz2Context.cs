using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tvz2api.Models
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

        public virtual DbSet<Izvrsitelj> Izvrsitelj { get; set; }
        public virtual DbSet<Kolegij> Kolegij { get; set; }
        public virtual DbSet<Odjel> Odjel { get; set; }
        public virtual DbSet<Pretplata> Pretplata { get; set; }
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
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Izvrsitelj>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

                entity.Property(e => e.UlogaIzvrsiteljaId).HasColumnName("UlogaIzvrsiteljaID");

                entity.Property(e => e.ZaposlenikId).HasColumnName("ZaposlenikID");

                entity.HasOne(d => d.Kolegij)
                    .WithMany(p => p.Izvrsitelj)
                    .HasForeignKey(d => d.KolegijId)
                    .HasConstraintName("FK__Izvrsitel__Koleg__5165187F");

                entity.HasOne(d => d.UlogaIzvrsitelja)
                    .WithMany(p => p.Izvrsitelj)
                    .HasForeignKey(d => d.UlogaIzvrsiteljaId)
                    .HasConstraintName("FK__Izvrsitel__Uloga__52593CB8");

                entity.HasOne(d => d.Zaposlenik)
                    .WithMany(p => p.Izvrsitelj)
                    .HasForeignKey(d => d.ZaposlenikId)
                    .HasConstraintName("FK__Izvrsitel__Zapos__5070F446");
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
                    .HasConstraintName("FK__Kolegij__SmjerID__3E52440B");
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
                    .HasConstraintName("FK__Pretplata__Koleg__5CD6CB2B");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Pretplata)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Pretplata__Stude__5BE2A6F2");
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
                    .HasConstraintName("FK__SidebarCo__Koleg__48CFD27E");
            });

            modelBuilder.Entity<SidebarContentFile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SidebarContentId).HasColumnName("SidebarContentID");

                entity.HasOne(d => d.SidebarContent)
                    .WithMany(p => p.SidebarContentFile)
                    .HasForeignKey(d => d.SidebarContentId)
                    .HasConstraintName("FK__SidebarCo__Sideb__4BAC3F29");
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
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SmjerId).HasColumnName("SmjerID");

                entity.HasOne(d => d.Smjer)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.SmjerId)
                    .HasConstraintName("FK__Student__SmjerID__5535A963");
            });

            modelBuilder.Entity<StudentKolegij>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.KolegijId).HasColumnName("KolegijID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Kolegij)
                    .WithMany(p => p.StudentKolegij)
                    .HasForeignKey(d => d.KolegijId)
                    .HasConstraintName("FK__StudentKo__Koleg__59063A47");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentKolegij)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__StudentKo__Stude__5812160E");
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
                    .HasConstraintName("FK__Vijest__KolegijI__44FF419A");

                entity.HasOne(d => d.Objavio)
                    .WithMany(p => p.Vijest)
                    .HasForeignKey(d => d.ObjavioId)
                    .HasConstraintName("FK__Vijest__ObjavioI__45F365D3");
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
                    .HasConstraintName("FK__Zaposleni__Odjel__4222D4EF");

                entity.HasOne(d => d.VrstaZaposljenja)
                    .WithMany(p => p.Zaposlenik)
                    .HasForeignKey(d => d.VrstaZaposljenjaId)
                    .HasConstraintName("FK__Zaposleni__Vrsta__412EB0B6");
            });
        }
    }
}
