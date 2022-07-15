﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoBo.Data;

namespace PhoBo.Migrations
{
    [DbContext(typeof(PhoBoContext))]
    [Migration("20220714182452_Phobo")]
    partial class Phobo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PhoBo.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("BookingRate")
                        .HasColumnType("real");

                    b.Property<int>("ConceptId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<float>("Duration")
                        .HasColumnType("real");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConceptId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PhotographerId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("PhoBo.Models.BookingConceptConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConceptId")
                        .HasColumnType("int");

                    b.Property<string>("DurationConfig")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhotographerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConceptId");

                    b.HasIndex("PhotographerId");

                    b.ToTable("BookingConceptConfig");
                });

            modelBuilder.Entity("PhoBo.Models.BookingConceptImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConfigId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigId");

                    b.ToTable("BookingConceptImage");
                });

            modelBuilder.Entity("PhoBo.Models.Concept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Concept");
                });

            modelBuilder.Entity("PhoBo.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PhoBo.Models.Customer", b =>
                {
                    b.HasBaseType("PhoBo.Models.User");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("PhoBo.Models.Photographer", b =>
                {
                    b.HasBaseType("PhoBo.Models.User");

                    b.Property<float>("Rate")
                        .HasColumnType("real");

                    b.ToTable("Photographer");
                });

            modelBuilder.Entity("PhoBo.Models.Booking", b =>
                {
                    b.HasOne("PhoBo.Models.Concept", "Concept")
                        .WithMany()
                        .HasForeignKey("ConceptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoBo.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoBo.Models.Photographer", "Photographer")
                        .WithMany()
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concept");

                    b.Navigation("Customer");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("PhoBo.Models.BookingConceptConfig", b =>
                {
                    b.HasOne("PhoBo.Models.Concept", "Concept")
                        .WithMany()
                        .HasForeignKey("ConceptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PhoBo.Models.Photographer", "Photographer")
                        .WithMany()
                        .HasForeignKey("PhotographerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concept");

                    b.Navigation("Photographer");
                });

            modelBuilder.Entity("PhoBo.Models.BookingConceptImage", b =>
                {
                    b.HasOne("PhoBo.Models.BookingConceptConfig", "Config")
                        .WithMany()
                        .HasForeignKey("ConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Config");
                });

            modelBuilder.Entity("PhoBo.Models.Customer", b =>
                {
                    b.HasOne("PhoBo.Models.User", null)
                        .WithOne()
                        .HasForeignKey("PhoBo.Models.Customer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PhoBo.Models.Photographer", b =>
                {
                    b.HasOne("PhoBo.Models.User", null)
                        .WithOne()
                        .HasForeignKey("PhoBo.Models.Photographer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
