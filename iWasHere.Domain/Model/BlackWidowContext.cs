using System;
using iWasHere.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Domain.Model
{
    public partial class BlackWidowContext : DbContext
    {
        public BlackWidowContext()
        {
        }

        public BlackWidowContext(DbContextOptions<BlackWidowContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DictionaryAttractionCategory> DictionaryAttractionCategory { get; set; }
        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }
        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }
        public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }
        public virtual DbSet<DictionaryCurrency> DictionaryCurrency { get; set; }
        public virtual DbSet<DictionaryOpenSeason> DictionaryOpenSeason { get; set; }
        public virtual DbSet<DictionaryTicket> DictionaryTicket { get; set; }
        public virtual DbSet<ExchangeRate> ExchangeRate { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TouristicObjective> TouristicObjective { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ts-internship-2019.database.windows.net;Initial Catalog=BlackWidow;Persist Security Info=False;User ID=sa_admin;Password=A123456a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<DictionaryAttractionCategory>(entity =>
            {
                entity.HasKey(e => e.AttractionCategoryId)
                    .HasName("PK__TAttract__904246DD5E41B0AB");

                entity.Property(e => e.AttractionCategoryId).ValueGeneratedNever();

                entity.Property(e => e.AttractionCategoryName)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__Dictiona__F2D21B768ABBE864");

                entity.Property(e => e.CityId).ValueGeneratedNever();

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.County)
                    .WithMany(p => p.DictionaryCity)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dictionar__Count__10566F31");
            });

            modelBuilder.Entity<DictionaryCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__TDiction__10D1609FEC86AEE1");

                entity.Property(e => e.CountryId).ValueGeneratedNever();

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DictionaryCounty>(entity =>
            {
                entity.HasKey(e => e.CountyId)
                    .HasName("PK__Dictiona__B68F9D9764B0071D");

                entity.Property(e => e.CountyId).ValueGeneratedNever();

                entity.Property(e => e.CountyName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.DictionaryCounty)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dictionar__Count__114A936A");
            });

            modelBuilder.Entity<DictionaryCurrency>(entity =>
            {
                entity.Property(e => e.DictionaryCurrencyId).ValueGeneratedNever();

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<DictionaryOpenSeason>(entity =>
            {
                entity.HasKey(e => e.OpenSeasonId)
                    .HasName("PK__TOpenSea__FF90875FDE8448CC");

                entity.Property(e => e.OpenSeasonId).ValueGeneratedNever();

                entity.Property(e => e.OpenSeasonType)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<DictionaryTicket>(entity =>
            {
                entity.Property(e => e.DictionaryTicketId).ValueGeneratedNever();

                entity.Property(e => e.TicketCategory)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.HasKey(e => e.DictionaryExchangeRateId)
                    .HasName("PK__Dictiona__143357EAD5F6A6C1");

                entity.Property(e => e.DictionaryExchangeRateId).ValueGeneratedNever();

                entity.Property(e => e.CurrentDate).HasColumnType("datetime");

                entity.Property(e => e.ExchangeRate1).HasColumnName("ExchangeRate");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.FeedbackId).ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(1056);

                entity.Property(e => e.CommentTitle).HasMaxLength(256);

                entity.Property(e => e.FeedbackName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.TouristicObjective)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.TouristicObjectiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__Touris__14270015");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Feedback__UserId__1BC821DD");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__TImage__7516F70CCD566FE6");

                entity.Property(e => e.ImageId).ValueGeneratedNever();

                entity.Property(e => e.Picture1).HasColumnName("Picture");

                entity.HasOne(d => d.TouristicObjective)
                    .WithMany(p => p.Picture)
                    .HasForeignKey(d => d.TouristicObjectiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Picture__Tourist__1332DBDC");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.DictionaryCurrency)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.DictionaryCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__Dictiona__17036CC0");

                entity.HasOne(d => d.DictionaryExchangeRate)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.DictionaryExchangeRateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__Dictiona__19DFD96B");

                entity.HasOne(d => d.DictionaryTicket)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.DictionaryTicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TTicket__Diction__5EBF139D");

                entity.HasOne(d => d.TouristicObjective)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TouristicObjectiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__Touristi__1DB06A4F");
            });

            modelBuilder.Entity<TouristicObjective>(entity =>
            {
                entity.HasIndex(e => e.TouristicObjectiveCode)
                    .HasName("UQ__Touristi__BE83950C09CA8056")
                    .IsUnique();

                entity.Property(e => e.TouristicObjectiveId).ValueGeneratedNever();

                entity.Property(e => e.TouristicObjectiveCode)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.TouristicObjectiveDescription)
                    .IsRequired()
                    .HasMaxLength(1056);

                entity.Property(e => e.TouristicObjectiveName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasOne(d => d.AttractionCategory)
                    .WithMany(p => p.TouristicObjective)
                    .HasForeignKey(d => d.AttractionCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TTouristi__Attra__68487DD7");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TouristicObjective)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Touristic__CityI__123EB7A3");

                entity.HasOne(d => d.OpenSeason)
                    .WithMany(p => p.TouristicObjective)
                    .HasForeignKey(d => d.OpenSeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TTouristi__OpenS__693CA210");
            });
        }
    }
}
