using Microsoft.EntityFrameworkCore;

namespace NotificationSystem.infrastructure.Models
{
    public partial class SchedulerContext : DbContext
    {
        public SchedulerContext(DbContextOptions<SchedulerContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyNotification> CompanyNotifications { get; set; }
        public virtual DbSet<CompanyType> CompanyTypes { get; set; }
        public virtual DbSet<Market> Markets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.Property(e => e.CompanyId)
                    .ValueGeneratedNever()
                    .HasColumnName("CompanyId").IsRequired();

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.CompanyNumber)
                    .IsRequired()
                    .HasMaxLength(10).IsFixedLength();

            });
            modelBuilder.Entity<Market>(entity =>
            {
                entity.HasKey(e => e.MarketId);

                entity.Property(e => e.MarketId)
                    .ValueGeneratedNever()
                    .HasColumnName("MarketId").IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.HasKey(e => e.CompanyTypeId);

                entity.Property(e => e.CompanyTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("CompanyTypeId").IsRequired();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
