﻿// <auto-generated />
using System;
using AnimalFriendsInsurance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimalFriendsInsurance.Data.Migrations
{
    [DbContext(typeof(AnimalFriendsInsuranceDataContext))]
    [Migration("20230114151730_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimalFriendsInsurance.Data.Entities.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PolicyReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer", "afi");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("AnimalFriendsInsurance.Data.Entities.CustomerDateOfBirthEntity", b =>
                {
                    b.HasBaseType("AnimalFriendsInsurance.Data.Entities.CustomerEntity");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.ToTable("CustomerDateOfBirth", "afi");
                });

            modelBuilder.Entity("AnimalFriendsInsurance.Data.Entities.CustomerEmailEntity", b =>
                {
                    b.HasBaseType("AnimalFriendsInsurance.Data.Entities.CustomerEntity");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("CustomerEmail", "afi");
                });

            modelBuilder.Entity("AnimalFriendsInsurance.Data.Entities.CustomerDateOfBirthEntity", b =>
                {
                    b.HasOne("AnimalFriendsInsurance.Data.Entities.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("AnimalFriendsInsurance.Data.Entities.CustomerDateOfBirthEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalFriendsInsurance.Data.Entities.CustomerEmailEntity", b =>
                {
                    b.HasOne("AnimalFriendsInsurance.Data.Entities.CustomerEntity", null)
                        .WithOne()
                        .HasForeignKey("AnimalFriendsInsurance.Data.Entities.CustomerEmailEntity", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
