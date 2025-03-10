﻿// <auto-generated />
using System;
using CrayonCloudSales.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CrayonCloudSales.Infrastructure.Migrations
{
    [DbContext(typeof(CloudSalesDbContext))]
    partial class CloudSalesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CrayonCloudSales.Core.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            IsActive = true,
                            Name = "Contoso Main Account"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 1,
                            IsActive = true,
                            Name = "Contoso Development"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2021, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CustomerId = 2,
                            IsActive = true,
                            Name = "Fabrikam Main Account"
                        });
                });

            modelBuilder.Entity("CrayonCloudSales.Core.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@contoso.com",
                            IsActive = true,
                            Name = "Contoso Ltd"
                        },
                        new
                        {
                            Id = 2,
                            Email = "admin@fabrikam.com",
                            IsActive = true,
                            Name = "Fabrikam Inc"
                        });
                });

            modelBuilder.Entity("CrayonCloudSales.Core.Models.SoftwareLicense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CcpSubscriptionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SoftwareServiceId")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("SoftwareLicenses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            CcpSubscriptionId = "SUB-001",
                            PurchaseDate = new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Quantity = 10,
                            SoftwareServiceId = 1,
                            State = 0,
                            ValidToDate = new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 2,
                            CcpSubscriptionId = "SUB-002",
                            PurchaseDate = new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Quantity = 5,
                            SoftwareServiceId = 2,
                            State = 0,
                            ValidToDate = new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CrayonCloudSales.Core.Models.Account", b =>
                {
                    b.HasOne("CrayonCloudSales.Core.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CrayonCloudSales.Core.Models.SoftwareLicense", b =>
                {
                    b.HasOne("CrayonCloudSales.Core.Models.Account", "Account")
                        .WithMany("SoftwareLicenses")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("CrayonCloudSales.Core.Models.Account", b =>
                {
                    b.Navigation("SoftwareLicenses");
                });

            modelBuilder.Entity("CrayonCloudSales.Core.Models.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
