using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tvz2api.Model
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
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Odjel> Odjel { get; set; }
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
                optionsBuilder.UseSqlServer("Server=.\\;Database=tvz2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Izvrsitelj>(entity =>
            {
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
                entity.Property(e => e.AkademskaGodina).IsUnicode(false);

                entity.Property(e => e.Cilj).IsUnicode(false);

                entity.Property(e => e.Ishodi).IsUnicode(false);

                entity.Property(e => e.Isvu).IsUnicode(false);

                entity.Property(e => e.Isvuekvivalencije).IsUnicode(false);

                entity.Property(e => e.IzvedbaNastave).IsUnicode(false);

                entity.Property(e => e.Literatura).IsUnicode(false);

                entity.Property(e => e.MaterijalniUvjeti).IsUnicode(false);

                entity.Property(e => e.NacinIzvodenjaAuditornih).IsUnicode(false);

                entity.Property(e => e.NacinIzvodenjaLaboratorijskih).IsUnicode(false);

                entity.Property(e => e.NacinIzvodenjaPredavanja).IsUnicode(false);

                entity.Property(e => e.NaciniPolaganja).IsUnicode(false);

                entity.Property(e => e.Napomena).IsUnicode(false);

                entity.Property(e => e.Naziv).IsUnicode(false);

                entity.Property(e => e.PracenjeRada).IsUnicode(false);

                entity.Property(e => e.Preduvjeti).IsUnicode(false);

                entity.Property(e => e.ProvjeraZnanja).IsUnicode(false);

                entity.Property(e => e.SadrzajAuditornih).IsUnicode(false);

                entity.Property(e => e.SadrzajLaboratorijskih).IsUnicode(false);

                entity.Property(e => e.SadrzajPredavanja).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.Url).IsUnicode(false);

                entity.Property(e => e.Uvjet).IsUnicode(false);

                entity.HasOne(d => d.Smjer)
                    .WithMany(p => p.Kolegij)
                    .HasForeignKey(d => d.SmjerId)
                    .HasConstraintName("FK__Kolegij__SmjerID__3E52440B");
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.Username).IsUnicode(false);
            });

            modelBuilder.Entity<Odjel>(entity =>
            {
                entity.Property(e => e.Naziv).IsUnicode(false);
            });

            modelBuilder.Entity<SidebarContent>(entity =>
            {
                entity.Property(e => e.Naslov).IsUnicode(false);

                entity.HasOne(d => d.Kolegij)
                    .WithMany(p => p.SidebarContent)
                    .HasForeignKey(d => d.KolegijId)
                    .HasConstraintName("FK__SidebarCo__Koleg__48CFD27E");
            });

            modelBuilder.Entity<SidebarContentFile>(entity =>
            {
                entity.Property(e => e.Naziv).IsUnicode(false);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.HasOne(d => d.SidebarContent)
                    .WithMany(p => p.SidebarContentFile)
                    .HasForeignKey(d => d.SidebarContentId)
                    .HasConstraintName("FK__SidebarCo__Sideb__4BAC3F29");
            });

            modelBuilder.Entity<Smjer>(entity =>
            {
                entity.Property(e => e.Naziv).IsUnicode(false);

                entity.Property(e => e.SkraceniNaziv).IsUnicode(false);

                entity.Property(e => e.Vanredno).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.ImagePath).IsUnicode(false);

                entity.Property(e => e.Ime).IsUnicode(false);

                entity.Property(e => e.Jmbag).IsUnicode(false);

                entity.Property(e => e.Prezime).IsUnicode(false);

                entity.HasOne(d => d.Smjer)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.SmjerId)
                    .HasConstraintName("FK__Student__SmjerID__5535A963");
            });

            modelBuilder.Entity<StudentKolegij>(entity =>
            {
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
                entity.Property(e => e.Naziv).IsUnicode(false);
            });

            modelBuilder.Entity<Vijest>(entity =>
            {
                entity.Property(e => e.Naslov).IsUnicode(false);

                entity.Property(e => e.Opis).IsUnicode(false);

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
                entity.Property(e => e.Naziv).IsUnicode(false);
            });

            modelBuilder.Entity<Zaposlenik>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.ImagePath).IsUnicode(false);

                entity.Property(e => e.Ime).IsUnicode(false);

                entity.Property(e => e.Prezime).IsUnicode(false);

                entity.Property(e => e.TitulaIspred).IsUnicode(false);

                entity.Property(e => e.TitulaIza).IsUnicode(false);

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
