﻿// <auto-generated />
using System;
using HotProperty_PropertyAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotProperty_PropertyAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221117045007_MakeSureTheConnectionStringIsCorrect")]
    partial class MakeSureTheConnectionStringIsCorrect
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HotProperty_PropertyAPI.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Area")
                        .HasColumnType("int");

                    b.Property<int>("AskingPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoBedroom")
                        .HasColumnType("int");

                    b.Property<int?>("NoToilet")
                        .HasColumnType("int");

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Suburb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Area = 591,
                            AskingPrice = 980000,
                            CreatedDate = new DateTime(2022, 11, 17, 15, 50, 7, 192, DateTimeKind.Local).AddTicks(2658),
                            ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                            Name = "11 James St",
                            NoBedroom = 3,
                            NoToilet = 4,
                            PostCode = "3084",
                            State = "VIC",
                            Suburb = "Heidelberg",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Area = 750,
                            AskingPrice = 1080000,
                            CreatedDate = new DateTime(2022, 11, 17, 15, 50, 7, 192, DateTimeKind.Local).AddTicks(2662),
                            ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                            Name = "16 Lily Crt",
                            NoBedroom = 4,
                            NoToilet = 2,
                            PostCode = "2011",
                            State = "NSW",
                            Suburb = "Frankston",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Area = 720,
                            AskingPrice = 688000,
                            CreatedDate = new DateTime(2022, 11, 17, 15, 50, 7, 192, DateTimeKind.Local).AddTicks(2666),
                            ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                            Name = "177 Wonderwomen Prd",
                            NoBedroom = 4,
                            NoToilet = 2,
                            PostCode = "4011",
                            State = "QLD",
                            Suburb = "Hans",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
