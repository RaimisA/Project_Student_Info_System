﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Student_Info_System.Database;

#nullable disable

namespace Project_Student_Info_System.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20240918185820_ChangedPkToNeverGenerate")]
    partial class ChangedPkToNeverGenerate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartmentLecture", b =>
                {
                    b.Property<string>("DepartmentCode")
                        .HasColumnType("nchar(6)");

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentCode", "LectureId");

                    b.HasIndex("LectureId");

                    b.ToTable("DepartmentLecture", (string)null);
                });

            modelBuilder.Entity("LectureStudent", b =>
                {
                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.HasKey("LectureId", "StudentNumber");

                    b.HasIndex("StudentNumber");

                    b.ToTable("LectureStudent", (string)null);
                });

            modelBuilder.Entity("Project_Student_Info_System.Database.Entities.Department", b =>
                {
                    b.Property<string>("DepartmentCode")
                        .HasMaxLength(6)
                        .HasColumnType("nchar(6)")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DepartmentCode");

                    b.HasIndex("DepartmentCode")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Project_Student_Info_System.Database.Entities.Lecture", b =>
                {
                    b.Property<int>("LectureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LectureId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("WeekDay")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("LectureId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("Project_Student_Info_System.Database.Entities.Student", b =>
                {
                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.Property<string>("DepartmentCode")
                        .IsRequired()
                        .HasColumnType("nchar(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StudentNumber");

                    b.HasIndex("DepartmentCode");

                    b.HasIndex("StudentNumber")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DepartmentLecture", b =>
                {
                    b.HasOne("Project_Student_Info_System.Database.Entities.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Student_Info_System.Database.Entities.Lecture", null)
                        .WithMany()
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LectureStudent", b =>
                {
                    b.HasOne("Project_Student_Info_System.Database.Entities.Lecture", null)
                        .WithMany()
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_Student_Info_System.Database.Entities.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project_Student_Info_System.Database.Entities.Student", b =>
                {
                    b.HasOne("Project_Student_Info_System.Database.Entities.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Project_Student_Info_System.Database.Entities.Department", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
