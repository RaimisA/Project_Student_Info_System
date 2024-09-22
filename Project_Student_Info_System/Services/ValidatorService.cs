using Project_Student_Info_System.Database.Entities;
using Project_Student_Info_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_Student_Info_System.Services
{
    public class ValidatorService : IValidatorService
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly ILectureService _lectureService;

        public ValidatorService(IStudentService studentService, IDepartmentService departmentService, ILectureService lectureService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
            _lectureService = lectureService;
        }

        public void ValidateStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Student cannot be null.");
            }

            ValidateStudentName(student.FirstName);
            ValidateStudentName(student.LastName);
            ValidateStudentNumber(student.StudentNumber);
            ValidateEmail(student.Email);
            ValidateUniqueStudentNumber(student.StudentNumber);
        }

        public void ValidateDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department), "Department cannot be null.");
            }

            ValidateDepartmentName(department.Name);
            ValidateDepartmentCode(department.DepartmentCode);
            ValidateUniqueDepartmentCode(department.DepartmentCode);
        }

        public void ValidateLecture(Lecture lecture)
        {
            if (lecture == null)
            {
                throw new ArgumentNullException(nameof(lecture), "Lecture cannot be null.");
            }

            ValidateLectureName(lecture.Name);
            ValidateLectureTime(lecture.StartTime, lecture.EndTime);
            ValidateUniqueLectureName(lecture.Name);
        }

        public void ValidateStudentName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            {
                throw new ArgumentException("Name must be at least 2 characters long.");
            }

            if (name.Length > 50)
            {
                throw new ArgumentException("Name must be no more than 50 characters long.");
            }

            if (!name.All(char.IsLetter))
            {
                throw new ArgumentException("Name must contain only letters.");
            }
        }

        public void ValidateStudentNumber(int studentNumber)
        {
            string studentNumberStr = studentNumber.ToString();
            if (studentNumberStr.Length != 8)
            {
                throw new ArgumentException("Student number must be exactly 8 digits.");
            }
        }

        public void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Email must be in a valid format.");
            }
        }

        public void ValidateDepartmentName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length > 100 || !Regex.IsMatch(name, @"^[a-zA-Z0-9]+$"))
            {
                throw new ArgumentException("Department name must be between 3 and 100 characters and contain only letters and numbers.");
            }
        }

        public void ValidateDepartmentCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || code.Length != 6 || !Regex.IsMatch(code, @"^[a-zA-Z0-9]{6}$"))
            {
                throw new ArgumentException("Department code must be exactly 6 characters and contain only letters and numbers.");
            }
        }

        public void ValidateLectureName(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 5)
            {
                throw new ArgumentException("Lecture name must be at least 5 characters long.");
            }
        }

        public void ValidateLectureStartTime(DateTime startTime)
        {
            if (startTime.Hour <= 0 || startTime.Hour >= 23)
            {
                throw new ArgumentException("Lecture time must be between 00:00 and 23:00.");
            }
        }

        public void ValidateLectureEndTime(DateTime endTime)
        {
            if (endTime.Hour <= 0 || endTime.Hour >= 23)
            {
                throw new ArgumentException("Lecture time must be between 00:00 and 23:00.");
            }
        }

        public void ValidateLectureTime(DateTime startTime, DateTime endTime)
        {
            if (startTime >= endTime)
            {
                throw new ArgumentException("Lecture start time must be before lecture end time.");
            }
        }

        public string ValidateWeekDay(string weekDay)
        {
            if (string.IsNullOrWhiteSpace(weekDay))
            {
                var weekdays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
                var random = new Random();
                return weekdays[random.Next(weekdays.Length)];
            }
            else
            {
                weekDay = char.ToUpper(weekDay[0]) + weekDay.Substring(1).ToLower();

                if (!new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" }.Contains(weekDay))
                {
                    throw new ArgumentException("Invalid day of the week. Valid days are Monday through Friday.");
                }
            }
            return weekDay;
        }
        public void ValidateLectureOverlap(Lecture newLecture, IEnumerable<Lecture> existingLectures)
        {
            foreach (var lecture in existingLectures)
            {
                if (newLecture.StartTime < lecture.EndTime && newLecture.EndTime > lecture.StartTime)
                {
                    throw new ArgumentException("Lecture times overlap.");
                }
            }
        }

        public void ValidateUniqueStudentNumber(int studentNumber)
        {
            var students = _studentService.GetAllStudents();
            if (students.Any(s => s.StudentNumber == studentNumber))
            {
                throw new ArgumentException("Student number must be unique.");
            }
        }

        public void ValidateUniqueDepartmentCode(string departmentCode)
        {
            var departments = _departmentService.GetAllDepartments();
            if (departments.Any(d => d.DepartmentCode.Equals(departmentCode, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Department code must be unique.");
            }
        }

        public void ValidateUniqueLectureName(string lectureName)
        {
            var lectures = _lectureService.GetAllLectures();
            if (lectures.Any(l => l.Name.Equals(lectureName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Lecture name must be unique.");
            }
        }

        public void ValidateDepartmentExists(string departmentCode)
        {
            if (string.IsNullOrWhiteSpace(departmentCode))
            {
                throw new ArgumentException("Department code cannot be null or empty.");
            }

            var department = _departmentService.GetAllDepartments().FirstOrDefault(d => d.DepartmentCode.Equals(departmentCode, StringComparison.OrdinalIgnoreCase));
            if (department == null)
            {
                throw new ArgumentException("Department not found.");
            }
        }

        public void ValidateStudentExists(int studentNumber)
        {
            var student = _studentService.GetAllStudents().FirstOrDefault(s => s.StudentNumber == studentNumber);
            if (student == null)
            {
                throw new ArgumentException("Student not found.");
            }
        }

        public void ValidateLectureDuration(DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime)
            {
                throw new ArgumentException("End time must be after start time.");
            }

            TimeSpan duration = endTime - startTime;
            if (duration.TotalMinutes < 30 || duration.TotalMinutes > 180)
            {
                throw new ArgumentException("Lecture duration must be between 30 and 180 minutes.");
            }
        }

        public void ValidateLectureExists(int lectureId)
        {
            var lecture = _lectureService.GetAllLectures().FirstOrDefault(l => l.LectureId == lectureId);
            if (lecture == null)
            {
                throw new ArgumentException("Lecture not found.");
            }
        }
    }
}
