﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Data.Entity;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(PawFundDbContext))]
    [Migration("20241127041801_fixCode")]
    partial class fixCode
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repository.Data.Entity.Adoption", b =>
                {
                    b.Property<string>("AdoptionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("AdoptionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AdoptionStatus")
                        .HasColumnType("int");

                    b.Property<string>("PetId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AdoptionId");

                    b.HasIndex("UserId");

                    b.ToTable("Adoptions");
                });

            modelBuilder.Entity("Repository.Data.Entity.Donation", b =>
                {
                    b.Property<string>("DonationId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShelterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DonationId");

                    b.HasIndex("ShelterId");

                    b.HasIndex("UserId");

                    b.ToTable("Donation");
                });

            modelBuilder.Entity("Repository.Data.Entity.Event", b =>
                {
                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventStatus")
                        .HasColumnType("int");

                    b.Property<string>("ShelterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EventId");

                    b.HasIndex("ShelterId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Repository.Data.Entity.Image", b =>
                {
                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PetId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UrlImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.HasIndex("PetId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Repository.Data.Entity.Payment", b =>
                {
                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DonationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("DonationId")
                        .IsUnique();

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Repository.Data.Entity.Pet", b =>
                {
                    b.Property<string>("PetId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdoptionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Ages")
                        .HasColumnType("int");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShelterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ShelterStatus")
                        .HasColumnType("int");

                    b.Property<string>("Species")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PetId");

                    b.HasIndex("AdoptionId");

                    b.HasIndex("ShelterId");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("Repository.Data.Entity.Shelter", b =>
                {
                    b.Property<string>("ShelterId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ShelterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShelterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ShelterId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Shelter");
                });

            modelBuilder.Entity("Repository.Data.Entity.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Repository.Data.Entity.UserEvent", b =>
                {
                    b.Property<string>("UserEventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RegistationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserEventId");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEvent");
                });

            modelBuilder.Entity("Repository.Data.Entity.Adoption", b =>
                {
                    b.HasOne("Repository.Data.Entity.User", "User")
                        .WithMany("Adoptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.Data.Entity.Donation", b =>
                {
                    b.HasOne("Repository.Data.Entity.Shelter", "Shelter")
                        .WithMany("Donations")
                        .HasForeignKey("ShelterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Repository.Data.Entity.User", "User")
                        .WithMany("Donations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Shelter");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.Data.Entity.Event", b =>
                {
                    b.HasOne("Repository.Data.Entity.Shelter", "Shelter")
                        .WithMany("Events")
                        .HasForeignKey("ShelterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Shelter");
                });

            modelBuilder.Entity("Repository.Data.Entity.Image", b =>
                {
                    b.HasOne("Repository.Data.Entity.Pet", "Pet")
                        .WithMany("Images")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Repository.Data.Entity.Payment", b =>
                {
                    b.HasOne("Repository.Data.Entity.Donation", "Donation")
                        .WithOne("Payment")
                        .HasForeignKey("Repository.Data.Entity.Payment", "DonationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Donation");
                });

            modelBuilder.Entity("Repository.Data.Entity.Pet", b =>
                {
                    b.HasOne("Repository.Data.Entity.Adoption", "Adoption")
                        .WithMany("Pets")
                        .HasForeignKey("AdoptionId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Repository.Data.Entity.Shelter", "Shelter")
                        .WithMany("Pets")
                        .HasForeignKey("ShelterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adoption");

                    b.Navigation("Shelter");
                });

            modelBuilder.Entity("Repository.Data.Entity.Shelter", b =>
                {
                    b.HasOne("Repository.Data.Entity.User", "User")
                        .WithOne("Shelter")
                        .HasForeignKey("Repository.Data.Entity.Shelter", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.Data.Entity.UserEvent", b =>
                {
                    b.HasOne("Repository.Data.Entity.Event", "Event")
                        .WithMany("UserEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Repository.Data.Entity.User", "User")
                        .WithMany("UserEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.Data.Entity.Adoption", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Repository.Data.Entity.Donation", b =>
                {
                    b.Navigation("Payment")
                        .IsRequired();
                });

            modelBuilder.Entity("Repository.Data.Entity.Event", b =>
                {
                    b.Navigation("UserEvents");
                });

            modelBuilder.Entity("Repository.Data.Entity.Pet", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Repository.Data.Entity.Shelter", b =>
                {
                    b.Navigation("Donations");

                    b.Navigation("Events");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Repository.Data.Entity.User", b =>
                {
                    b.Navigation("Adoptions");

                    b.Navigation("Donations");

                    b.Navigation("Shelter")
                        .IsRequired();

                    b.Navigation("UserEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
