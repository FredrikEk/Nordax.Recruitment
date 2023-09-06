﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nordax.Bank.Recruitment.DataAccess;

namespace Nordax.Bank.Recruitment.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230906204229_LoanApplication")]
    partial class LoanApplication
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UploadedFileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UploadedFileId")
                        .IsUnique();

                    b.ToTable("LoanApplications");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("SignUpDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.UploadedFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("UploadedFiles");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication", b =>
                {
                    b.HasOne("Nordax.Bank.Recruitment.DataAccess.Entities.UploadedFile", "UploadedFile")
                        .WithOne("LoanApplication")
                        .HasForeignKey("Nordax.Bank.Recruitment.DataAccess.Entities.LoanApplication", "UploadedFileId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("UploadedFile");
                });

            modelBuilder.Entity("Nordax.Bank.Recruitment.DataAccess.Entities.UploadedFile", b =>
                {
                    b.Navigation("LoanApplication");
                });
#pragma warning restore 612, 618
        }
    }
}