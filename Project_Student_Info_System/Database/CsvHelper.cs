using Project_Student_Info_System.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Student_Info_System.Database
{
    public class CsvHelper
    {
        public List<List<string>> ReadCsv(string filePath)
        {
            var records = new List<List<string>>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var values = line.Split(',').ToList();
                records.Add(values);
            }

            return records;
        }

        public List<Department> GetDepartments(string filePath)
        {
            return ReadCsv(filePath)
                .Select(d => new Department
                {
                    DepartmentCode = d[0],
                    Name = d[1]
                }).ToList();
        }

        public List<Lecture> GetLectures(string filePath)
        {
            return ReadCsv(filePath)
                .Select(l => new Lecture
                {
                    LectureId = int.Parse(l[0]),
                    Name = l[1],
                    WeekDay = l[2],
                    StartTime = DateTime.Parse(l[3]),
                    EndTime = DateTime.Parse(l[4])
                }).ToList();
        }

        public List<Student> GetStudents(string filePath)
        {
            return ReadCsv(filePath)
                .Select(s => new Student
                {
                    StudentNumber = int.Parse(s[0]),
                    FirstName = s[1],
                    LastName = s[2],
                    Email = s[3],
                    DepartmentCode = s[4]
                }).ToList();
        }

        public void SeedDepartmentLectures(string filePath, List<Department> departments, List<Lecture> lectures)
        {
            var departmentLectures = ReadCsv(filePath);
            foreach (var dl in departmentLectures)
            {
                var department = departments.First(d => d.DepartmentCode == dl[0]);
                var lecture = lectures.First(l => l.LectureId == int.Parse(dl[1]));
                department.Lectures.Add(lecture);
            }
        }

        public void SeedLectureStudents(string filePath, List<Lecture> lectures, List<Student> students)
        {
            var lectureStudents = ReadCsv(filePath);
            foreach (var ls in lectureStudents)
            {
                var lecture = lectures.First(l => l.LectureId == int.Parse(ls[0]));
                var student = students.First(s => s.StudentNumber == int.Parse(ls[1]));
                lecture.Students.Add(student);
            }
        }
    }
}
