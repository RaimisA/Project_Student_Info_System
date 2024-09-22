﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Student_Info_System.Database;

#nullable disable

namespace Project_Student_Info_System.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasData(
                        new
                        {
                            DepartmentCode = "CS0050",
                            LectureId = 1
                        },
                        new
                        {
                            DepartmentCode = "MATH20",
                            LectureId = 2
                        },
                        new
                        {
                            DepartmentCode = "PHYS30",
                            LectureId = 3
                        },
                        new
                        {
                            DepartmentCode = "CS0050",
                            LectureId = 3
                        });
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

                    b.HasData(
                        new
                        {
                            LectureId = 1,
                            StudentNumber = 10001001
                        },
                        new
                        {
                            LectureId = 2,
                            StudentNumber = 10001001
                        },
                        new
                        {
                            LectureId = 2,
                            StudentNumber = 10001002
                        },
                        new
                        {
                            LectureId = 3,
                            StudentNumber = 10001002
                        },
                        new
                        {
                            LectureId = 1,
                            StudentNumber = 10001003
                        },
                        new
                        {
                            LectureId = 3,
                            StudentNumber = 10001003
                        });
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

                    b.HasData(
                        new
                        {
                            DepartmentCode = "CS0050",
                            Name = "Computer Science"
                        },
                        new
                        {
                            DepartmentCode = "MATH20",
                            Name = "Mathematics"
                        },
                        new
                        {
                            DepartmentCode = "PHYS30",
                            Name = "Physics"
                        });
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

                    b.HasData(
                        new
                        {
                            LectureId = 1,
                            EndTime = new DateTime(2024, 9, 19, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Name = "Materials Science",
                            StartTime = new DateTime(2024, 9, 19, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            WeekDay = "Monday"
                        },
                        new
                        {
                            LectureId = 2,
                            EndTime = new DateTime(2024, 9, 19, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            Name = "Machine Design",
                            StartTime = new DateTime(2024, 9, 19, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            WeekDay = "Tuesday"
                        },
                        new
                        {
                            LectureId = 3,
                            EndTime = new DateTime(2024, 9, 19, 15, 30, 0, 0, DateTimeKind.Unspecified),
                            Name = "Thermodynamics",
                            StartTime = new DateTime(2024, 9, 19, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            WeekDay = "Wednesday"
                        });
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

                    b.HasData(
                        new
                        {
                            StudentNumber = 10001001,
                            DepartmentCode = "CS0050",
                            Email = "ansel.reed@example.com",
                            FirstName = "Ansel",
                            LastName = "Reed"
                        },
                        new
                        {
                            StudentNumber = 10001002,
                            DepartmentCode = "MATH20",
                            Email = "greta.hale@example.com",
                            FirstName = "Greta",
                            LastName = "Hale"
                        },
                        new
                        {
                            StudentNumber = 10001003,
                            DepartmentCode = "PHYS30",
                            Email = "silas.lang@example.com",
                            FirstName = "Silas",
                            LastName = "Lang"
                        });
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
