using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<StudentAid> StudentAids { get; set; }
        public DbSet<ScienceClub> ScienceClubs { get; set; }
        public DbSet<UserScienceClub> UserScienceClubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(p => p.ID);
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();

            modelBuilder.Entity<StudentAid>().HasKey(p => p.ID);
            modelBuilder.Entity<StudentAid>().HasOne(p => p.User).WithMany(p => p.StudentAids).HasForeignKey(p => p.UserID);
            modelBuilder.Entity<StudentAid>().HasOne(p => p.ScienceClub).WithMany(p => p.StudentAids).HasForeignKey(p => p.ScienceClubID);
            modelBuilder.Entity<StudentAid>().Property(p => p.Count).HasDefaultValue(1);
            modelBuilder.Entity<StudentAid>().Property(p => p.DateTime).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<ScienceClub>().HasKey(p => p.ID);
            modelBuilder.Entity<ScienceClub>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<ScienceClub>().HasIndex(p => p.Name).IsUnique();

            modelBuilder.Entity<UserScienceClub>().HasKey(p => p.ID);
            modelBuilder.Entity<UserScienceClub>().HasOne(p => p.User).WithMany(p => p.Clubs).HasForeignKey(p => p.UserID);
            modelBuilder.Entity<UserScienceClub>().HasOne(p => p.ScienceClub).WithMany(p => p.Users).HasForeignKey(p => p.ScienceClubID);

            
        }
    }
}
