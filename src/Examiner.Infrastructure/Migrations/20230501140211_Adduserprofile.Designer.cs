﻿// <auto-generated />
using System;
using Examiner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examiner.Infrastructure.Migrations
{
    [DbContext(typeof(ExaminerContext))]
    [Migration("20230501140211_Adduserprofile")]
    partial class Adduserprofile
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Examiner.Domain.Entities.Authentication.CodeVerification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Attempts")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Expired")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int");

                    b.Property<bool>("HasVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSent")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("CodeVerifications");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Content.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SubjectCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SubjectCategoryId");

                    b.ToTable("Subjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1380),
                            SubjectCategoryId = 1,
                            Title = "Chemistry"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1416),
                            SubjectCategoryId = 1,
                            Title = "Physics"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1461),
                            SubjectCategoryId = 1,
                            Title = "Computer Science"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1488),
                            SubjectCategoryId = 2,
                            Title = "History"
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1515),
                            SubjectCategoryId = 2,
                            Title = "Government"
                        },
                        new
                        {
                            Id = 6,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1564),
                            SubjectCategoryId = 2,
                            Title = "Economics"
                        },
                        new
                        {
                            Id = 7,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1593),
                            SubjectCategoryId = 3,
                            Title = "Sociology"
                        },
                        new
                        {
                            Id = 8,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1619),
                            SubjectCategoryId = 3,
                            Title = "Geography"
                        },
                        new
                        {
                            Id = 9,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1654),
                            SubjectCategoryId = 3,
                            Title = "Mass communication"
                        });
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Content.SubjectCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SubjectCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1228),
                            Title = "Science"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1314),
                            Title = "Art"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2023, 5, 1, 15, 2, 11, 188, DateTimeKind.Local).AddTicks(1342),
                            Title = "Social Science"
                        });
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Notifications.Emails.KickboxVerification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("AcceptAll")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DidYouMean")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Disposable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Domain")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("Free")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsValidEmail")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Message")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Reason")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Result")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("Role")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("Sendex")
                        .HasColumnType("double");

                    b.Property<bool>("Success")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SupportingMessage")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("User")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("KickboxVerifications");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Users.UserIdentity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastEmailVerification")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserIdentities");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Users.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("LastAvailability")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastMobilePhoneVerification")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("ProfilePhotoPath")
                        .HasColumnType("longtext");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("SubjectUserProfile", b =>
                {
                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserProfilesId")
                        .HasColumnType("char(36)");

                    b.HasKey("SubjectsId", "UserProfilesId");

                    b.HasIndex("UserProfilesId");

                    b.ToTable("SubjectUserProfile");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Authentication.CodeVerification", b =>
                {
                    b.HasOne("Examiner.Domain.Entities.Users.UserIdentity", "User")
                        .WithOne("CodeVerification")
                        .HasForeignKey("Examiner.Domain.Entities.Authentication.CodeVerification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Content.Subject", b =>
                {
                    b.HasOne("Examiner.Domain.Entities.Content.SubjectCategory", "SubjectCategory")
                        .WithMany("Subjects")
                        .HasForeignKey("SubjectCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubjectCategory");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Users.UserProfile", b =>
                {
                    b.HasOne("Examiner.Domain.Entities.Users.UserIdentity", "User")
                        .WithOne("UserProfile")
                        .HasForeignKey("Examiner.Domain.Entities.Users.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SubjectUserProfile", b =>
                {
                    b.HasOne("Examiner.Domain.Entities.Content.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examiner.Domain.Entities.Users.UserProfile", null)
                        .WithMany()
                        .HasForeignKey("UserProfilesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Content.SubjectCategory", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Users.UserIdentity", b =>
                {
                    b.Navigation("CodeVerification");

                    b.Navigation("UserProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
