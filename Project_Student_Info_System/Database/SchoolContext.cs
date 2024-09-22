using Microsoft.EntityFrameworkCore;
using Project_Student_Info_System.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Student_Info_System.Database
{
    public class SchoolContext : DbContext
    {
        private bool seedData = true;
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }

        public SchoolContext() : base() { }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=CSharpMokymai_SchoolDb;Trusted_Connection=True;");
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Department Configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(d => d.DepartmentCode);
                entity.Property(d => d.DepartmentCode)
                      .ValueGeneratedNever();

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.DepartmentCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.HasIndex(d => d.DepartmentCode).IsUnique(); // Ensure it is Unique

                entity.HasMany(d => d.Students)
                    .WithOne(s => s.Department)
                    .HasForeignKey(s => s.DepartmentCode);

                entity.HasMany(d => d.Lectures)
                    .WithMany(l => l.Departments)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentLecture",
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                        j => j.HasOne<Department>().WithMany().HasForeignKey("DepartmentCode"),
                        j =>
                        {
                            j.HasKey("DepartmentCode", "LectureId");
                            j.ToTable("DepartmentLecture");
                        }
                    );
            });

            // Lecture Configuration
            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasKey(l => l.LectureId);

                entity.Property(l => l.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(l => l.WeekDay)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(l => l.StartTime)
                    .IsRequired();

                entity.Property(l => l.EndTime)
                    .IsRequired();

                entity.HasIndex(l => l.Name).IsUnique(); // Ensure Name is unique

                entity.HasMany(l => l.Departments)
                    .WithMany(d => d.Lectures)
                    .UsingEntity<Dictionary<string, object>>(
                        "DepartmentLecture",
                        j => j.HasOne<Department>().WithMany().HasForeignKey("DepartmentCode"),
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                        j =>
                        {
                            j.HasKey("DepartmentCode", "LectureId");
                            j.ToTable("DepartmentLecture");
                        }
                    );

                entity.HasMany(l => l.Students)
                    .WithMany(s => s.Lectures)
                    .UsingEntity<Dictionary<string, object>>(
                        "LectureStudent",
                        j => j.HasOne<Student>().WithMany().HasForeignKey("StudentNumber"),
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                        j =>
                        {
                            j.HasKey("LectureId", "StudentNumber");
                            j.ToTable("LectureStudent");
                        }
                    );
            });

            // Student Configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.StudentNumber);

                entity.Property(s => s.StudentNumber)
                      .ValueGeneratedNever();

                entity.Property(s => s.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(s => s.StudentNumber).IsUnique(); // Ensure Number is unique

                entity.HasOne(s => s.Department)
                    .WithMany(d => d.Students)
                    .HasForeignKey(s => s.DepartmentCode);
                    

                entity.HasMany(s => s.Lectures)
                    .WithMany(l => l.Students)
                    .UsingEntity<Dictionary<string, object>>(
                        "LectureStudent",
                        j => j.HasOne<Lecture>().WithMany().HasForeignKey("LectureId"),
                        j => j.HasOne<Student>().WithMany().HasForeignKey("StudentNumber"),
                        j =>
                        {
                            j.HasKey("LectureId", "StudentNumber");
                            j.ToTable("LectureStudent");
                        }
                    );
            });

            // Seed data
            if (seedData)
            {
                SeedData(modelBuilder);
            }
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var csvHelper = new CsvHelper();

            // Define the relative paths to the CSV files
            string[] csvFiles = {
                "Database/InitialData/Departments.csv",
                "Database/InitialData/Lectures.csv",
                "Database/InitialData/Students.csv",
                "Database/InitialData/DepartmentLectures.csv",
                "Database/InitialData/LectureStudents.csv"
            };

            // Construct absolute paths using AppContext.BaseDirectory
            string baseDirectory = AppContext.BaseDirectory;
            string[] absoluteCsvFiles = csvFiles.Select(file => Path.Combine(baseDirectory, file)).ToArray();

            // Read data from CSV files
            var departments = csvHelper.GetDepartments(absoluteCsvFiles[0]);
            var lectures = csvHelper.GetLectures(absoluteCsvFiles[1]);
            var students = csvHelper.GetStudents(absoluteCsvFiles[2]);

            // Seed data into the model
            modelBuilder.Entity<Department>().HasData(departments);
            modelBuilder.Entity<Lecture>().HasData(lectures);
            modelBuilder.Entity<Student>().HasData(students);

            // Seed relationships
            var departmentLectures = csvHelper.ReadCsv(absoluteCsvFiles[3])
                .Select(dl => new { DepartmentCode = dl[0], LectureId = int.Parse(dl[1]) })
                .ToArray();
            modelBuilder.Entity("DepartmentLecture").HasData(departmentLectures);

            var lectureStudents = csvHelper.ReadCsv(absoluteCsvFiles[4])
                .Select(ls => new { LectureId = int.Parse(ls[0]), StudentNumber = int.Parse(ls[1]) })
                .ToArray();
            modelBuilder.Entity("LectureStudent").HasData(lectureStudents);
        }
    }
}
