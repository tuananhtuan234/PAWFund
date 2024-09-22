using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entity
{
    public partial class PawFundDbContext: DbContext
    {
        public PawFundDbContext()
        {

        }

        public PawFundDbContext(DbContextOptions<PawFundDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Adoption> Adoptions { get; set; }
        public virtual DbSet<Shelter> Shelters { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<Event> Events { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //    var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlServer("Server=localhost;Database=PawFundDB;Uid=sa;Password=12345;MultipleActiveResultSets=true;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            // User - Donation
            modelBuilder.Entity<User>()
                .HasMany(u => u.Donations).WithOne(d => d.User).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
            // User - Shelter
            modelBuilder.Entity<User>()
                .HasOne(u => u.Shelter).WithOne(s => s.User).HasForeignKey<User>(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
            // User - Adoption
            modelBuilder.Entity<User>()
                .HasMany(u => u.Adoptions).WithOne(a => a.User).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
            // User - UserEvent
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserEvents).WithOne(aue=> aue.User).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);

            //donation
            //Donation - Shelter
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Shelter).WithMany(s => s.Donations).HasForeignKey(s => s.ShelterId).OnDelete(DeleteBehavior.Restrict);
            //Donation - Payment
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Payment).WithOne(p => p.Donation).HasForeignKey<Donation>(d => d.DonationId).OnDelete(DeleteBehavior.Restrict);

            //Shelter
            // Shelter - Adoption
            modelBuilder.Entity<Shelter>()
                .HasMany(s => s.Adoptions).WithOne(a => a.Shelter).HasForeignKey(s => s.ShelterId).OnDelete(DeleteBehavior.Restrict);
            // Shelter - Pet
            modelBuilder.Entity<Shelter>()
                .HasMany(s => s.Pets).WithOne(p => p.Shelter).HasForeignKey(s => s.ShelterId).OnDelete(DeleteBehavior.Restrict);
            // Shelter - Event
            modelBuilder.Entity<Shelter>()
                .HasMany(s => s.Events).WithOne(e => e.Shelter).HasForeignKey(s => s.ShelterId).OnDelete(DeleteBehavior.Restrict);

            // Event
            // Event - UserEvent
            modelBuilder.Entity<Event>()
                .HasMany(e => e.UserEvents).WithOne(ue => ue.Event).HasForeignKey(e => e.EventId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
