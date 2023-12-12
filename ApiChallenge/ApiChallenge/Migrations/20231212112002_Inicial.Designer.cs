﻿// <auto-generated />
using System;
using ApiChallenge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiChallenge.Migrations
{
    [DbContext(typeof(ServerContext))]
    [Migration("20231212112002_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("ApiChallenge.Models.Recycler", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("days")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("run")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Recyclers");

                    b.HasData(
                        new
                        {
                            id = new Guid("bbc5a357-95c8-4712-ab96-5215c4a45f74"),
                            days = 0,
                            run = false
                        });
                });

            modelBuilder.Entity("ApiChallenge.Models.Server", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ip")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("port")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("ApiChallenge.Models.Video", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("path")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("serverId")
                        .HasColumnType("TEXT");

                    b.Property<long>("size")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("serverId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("ApiChallenge.Models.Video", b =>
                {
                    b.HasOne("ApiChallenge.Models.Server", null)
                        .WithMany("Video")
                        .HasForeignKey("serverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiChallenge.Models.Server", b =>
                {
                    b.Navigation("Video");
                });
#pragma warning restore 612, 618
        }
    }
}