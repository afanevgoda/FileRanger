﻿// <auto-generated />
using System;
using DAL.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220609091918_ItemStatus")]
    partial class ItemStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("rowid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("extension");

                    b.Property<string>("FullPath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("fullPath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ParentPath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("parentPath");

                    b.Property<int>("SnapshotId")
                        .HasColumnType("integer")
                        .HasColumnName("snapshotId");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("SnapshotId");

                    b.ToTable("file", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("rowid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FullPath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("fullPath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ParentPath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("parentPath");

                    b.Property<int>("SnapshotId")
                        .HasColumnType("integer")
                        .HasColumnName("snapshotId");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("SnapshotId");

                    b.ToTable("folder", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Snapshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("rowid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("Drive")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("drive");

                    b.Property<string>("Hostname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hostname");

                    b.Property<int>("Result")
                        .HasColumnType("integer")
                        .HasColumnName("result");

                    b.HasKey("Id");

                    b.ToTable("snapshot", (string)null);
                });

            modelBuilder.Entity("DAL.Models.File", b =>
                {
                    b.HasOne("DAL.Models.Snapshot", "Snapshot")
                        .WithMany("Files")
                        .HasForeignKey("SnapshotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snapshot");
                });

            modelBuilder.Entity("DAL.Models.Folder", b =>
                {
                    b.HasOne("DAL.Models.Snapshot", "Snapshot")
                        .WithMany("Folders")
                        .HasForeignKey("SnapshotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snapshot");
                });

            modelBuilder.Entity("DAL.Models.Snapshot", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Folders");
                });
#pragma warning restore 612, 618
        }
    }
}
