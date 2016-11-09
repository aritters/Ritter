using Aritter.Infra.Crosscutting.Exceptions;
using Aritter.Infra.Crosscutting.Logging;
using Aritter.Infra.Data.Seedwork;
using Aritter.Security.Domain.Users.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Aritter.Security.Infra.Data
{
    public class AritterContext : DbContext, IQueryableUnitOfWork
    {
        private bool disposed;
        private IDbContextTransaction transaction;
        private readonly IConfiguration configuration;

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Credential> Credentials { get; set; }

        public AritterContext(IConfiguration configuration)
            : base()
        {
            this.configuration = configuration;
        }

        public AritterContext(DbContextOptions options)
            : base(options)
        {
        }

        public void BeginTransaction()
        {
            transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction == null)
            {
                ThrowHelper.ThrowApplicationException("Transaction is not open");
            }

            Database.CommitTransaction();
        }

        public void Rollback()
        {
            if (transaction == null)
            {
                ThrowHelper.ThrowApplicationException("Transaction is not open");
            }

            Database.RollbackTransaction();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

                entity.Property(e => e.UID)
                    .IsRequired();

                entity.HasIndex(e => e.Username)
                    .IsUnique();

                entity.Property(e => e.Username)
                    .HasMaxLength(256);

                entity.HasOne(p => p.Credential)
                    .WithOne(i => i.User)
                    .HasForeignKey<Credential>(b => b.UserId);
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(p => p.UserId);

                entity.Property(p => p.UserId)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasMaxLength(256);

                entity.Property(e => e.CreateDate)
                    .IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //LoggerFactory.SetCurrent(new NLogFactory());

                optionsBuilder.EnableSensitiveDataLogging();
                //optionsBuilder.UseLoggerFactory(LoggerFactory.Current());

                optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //optionsBuilder.UseNpgsql(ApplicationSettings.ConnectionString("Aritter_Npgsql"));
            }
        }

        public override void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            base.Dispose();

            if (!disposed && disposing)
            {
                if (Users != null)
                    Users = null;

                if (Credentials != null)
                    Credentials = null;

                disposed = true;
            }
        }
    }
}
