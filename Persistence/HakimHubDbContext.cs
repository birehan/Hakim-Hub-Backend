using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Common;

namespace Persistence
{
    public class HakimHubDbContext : IdentityDbContext<AppUser>
    {
        public HakimHubDbContext(DbContextOptions<HakimHubDbContext> options)
         : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HakimHubDbContext).Assembly);

            // Unqiue service name
            modelBuilder.Entity<Service>()
             .HasIndex(s => s.ServiceName)
             .IsUnique();

            modelBuilder.Entity<Speciality>()
           .HasIndex(s => s.Name)
           .IsUnique();

            // address to institution
            modelBuilder.Entity<InstitutionProfile>()
            .HasOne(e => e.Address)
            .WithOne(d => d.Institution)
            .HasForeignKey<InstitutionProfile>(e => e.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

            // doctor profile to phot
            modelBuilder.Entity<DoctorProfile>()
                .HasOne(d => d.Photo)
                .WithOne()
                .HasForeignKey<Photo>(x => x.DoctorProfileId);


            // institiution logo with photo
            modelBuilder.Entity<InstitutionProfile>()
                .HasOne(d => d.Logo)
                .WithOne()
                .HasForeignKey<Photo>(x => x.LogoId);

            // institiution banner with photo
            modelBuilder.Entity<InstitutionProfile>()
                .HasOne(d => d.Banner)
                .WithOne()
                .HasForeignKey<Photo>(x => x.BannerId);
            //
            modelBuilder.Entity<DoctorProfile>()
                .HasOne(d => d.MainInstitution)
                .WithMany()
                .HasForeignKey(d => d.MainInstitutionId)
                .OnDelete(DeleteBehavior.Restrict);

            // doctor availability
            modelBuilder.Entity<DoctorProfile>()
            .HasMany(e => e.DoctorAvailabilities)
            .WithOne(d => d.Doctor)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

            // doctor to institution
            modelBuilder.Entity<DoctorProfile>()
            .HasMany(e => e.Institutions)
            .WithMany(d => d.Doctors);

            // education availability
            modelBuilder.Entity<DoctorProfile>()
            .HasMany(e => e.Educations)
            .WithOne(d => d.Doctor)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

            // experience availability
            modelBuilder.Entity<DoctorProfile>()
            .HasMany(e => e.Experiences)
            .WithOne(d => d.Doctor)
            .HasForeignKey(e => e.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

            // speciality availability
            modelBuilder.Entity<DoctorProfile>()
            .HasMany(e => e.Specialities)
            .WithMany(d => d.Doctors);
            // 
            modelBuilder.Entity<DoctorProfile>()
            .HasOne(e => e.Photo)
            .WithOne()
            .HasForeignKey<DoctorProfile>(e => e.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);

            // 
            modelBuilder.Entity<Education>()
            .HasOne(e => e.InstitutionLogo)
            .WithOne()
            .HasForeignKey<Education>(e => e.InstitutionLogoId)
            .OnDelete(DeleteBehavior.Cascade);
            // 
            modelBuilder.Entity<InstitutionProfile>()
            .HasOne(e => e.InstitutionAvailability)
            .WithOne(d => d.Institution)
            .HasForeignKey<InstitutionAvailability>(e => e.InstitutionId)
            .OnDelete(DeleteBehavior.Cascade);

            // address to institution
            modelBuilder.Entity<InstitutionProfile>()
            .HasMany(e => e.Services)
            .WithMany(d => d.Institutions);

            // address to institution
            modelBuilder.Entity<InstitutionProfile>()
            .HasMany(e => e.Photos)
            .WithOne()
            .HasForeignKey("InstitutionProfileId")
            .OnDelete(DeleteBehavior.Cascade);

            // address to institution
            modelBuilder.Entity<InstitutionProfile>()
            .HasOne(e => e.Banner)
            .WithOne()
            .HasForeignKey<InstitutionProfile>(e => e.BannerId)
            .OnDelete(DeleteBehavior.Cascade);

            // address to institution
            modelBuilder.Entity<InstitutionProfile>()
            .HasOne(e => e.Logo)
            .WithOne()
            .HasForeignKey<InstitutionProfile>(e => e.LogoId)
            .OnDelete(DeleteBehavior.Cascade);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<InstitutionProfile> InstitutioProfiles { get; set; }
        public DbSet<InstitutionAvailability> InstitutionAvailabilities { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}