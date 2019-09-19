using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Web.Models
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

        public virtual DbSet<DictionaryContinent> DictionaryContinent { get; set; }

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

            modelBuilder.Entity<DictionaryContinent>(entity =>
            {
                entity.HasKey(e => e.ContinentId)
                    .HasName("PK__Dictiona__7E5220E1D68A22F5");

                entity.Property(e => e.ContinentName)
                    .IsRequired()
                    .HasMaxLength(450);
            });
        }
    }
}
