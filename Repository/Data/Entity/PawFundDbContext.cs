using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entity
{
    public partial class PawFundDbContext : DbContext
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
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<History> Histories { get; set; }


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
     => optionsBuilder.UseSqlServer("Server=(local);Database=PawFundDB;Uid=sa;Password=12345;MultipleActiveResultSets=true;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasOne(u => u.Shelter)
            .WithOne(s => s.User)
            .HasForeignKey<Shelter>(s => s.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Adoption>()
            .HasOne(a => a.User)
            .WithMany(u => u.Adoptions)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Pet>()
            .HasOne(a => a.Adoption)
            .WithMany(s => s.Pets)
            .HasForeignKey(a => a.AdoptionId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Donation>()
            .HasOne(d => d.User)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Donation>()
            .HasOne(d => d.Shelter)
            .WithMany(s => s.Donations)
            .HasForeignKey(d => d.ShelterId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Event>()
            .HasOne(e => e.Shelter)
            .WithMany(s => s.Events)
            .HasForeignKey(e => e.ShelterId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserEvent>()
            .HasOne(ue => ue.User)
            .WithMany(u => u.UserEvents)
            .HasForeignKey(ue => ue.UserId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserEvent>()
            .HasOne(ue => ue.Event)
            .WithMany(e => e.UserEvents)
            .HasForeignKey(ue => ue.EventId)
            .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Payment>()
            .HasOne(p => p.Donation)
            .WithOne(d => d.Payment)
            .HasForeignKey<Payment>(p => p.DonationId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Image>()
            .HasOne(i => i.Pet)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.PetId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<History>()
            .HasOne(h => h.Adoption)  
            .WithMany(a => a.Histories)  
            .HasForeignKey(h => h.AdoptionId)  
            .OnDelete(DeleteBehavior.NoAction); 


            // Add more Fluent API configurations as needed


            base.OnModelCreating(modelBuilder);
        }
    }
}
