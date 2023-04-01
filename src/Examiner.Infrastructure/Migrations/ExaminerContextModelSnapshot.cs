﻿// <auto-generated />
using System;
using Examiner.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examiner.Infrastructure.Migrations
{
    [DbContext(typeof(ExaminerContext))]
    partial class ExaminerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("CanResend")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Expired")
                        .HasColumnType("tinyint(1)");

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

            modelBuilder.Entity("Examiner.Domain.Entities.Authentication.CodeVerificationHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CodeVerificationId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CodeVerificationId");

                    b.ToTable("CodeVerificationHistories");
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

            modelBuilder.Entity("Examiner.Domain.Entities.Users.User", b =>
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

                    b.Property<string>("EmailVerificationToken")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastEmailVerification")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastMobilePhoneVerification")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("LastPasswordReset")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Authentication.CodeVerification", b =>
                {
                    b.HasOne("Examiner.Domain.Entities.Users.User", "User")
                        .WithOne("CodeVerification")
                        .HasForeignKey("Examiner.Domain.Entities.Authentication.CodeVerification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Authentication.CodeVerificationHistory", b =>
                {
                    b.HasOne("Examiner.Domain.Entities.Authentication.CodeVerification", "CodeVerification")
                        .WithMany("CodeVerificationHistories")
                        .HasForeignKey("CodeVerificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CodeVerification");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Authentication.CodeVerification", b =>
                {
                    b.Navigation("CodeVerificationHistories");
                });

            modelBuilder.Entity("Examiner.Domain.Entities.Users.User", b =>
                {
                    b.Navigation("CodeVerification");
                });
#pragma warning restore 612, 618
        }
    }
}
