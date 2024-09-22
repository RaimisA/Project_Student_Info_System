using Project_Student_Info_System.Database.Entities;
using Project_Student_Info_System.Services;
using Project_Student_Info_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Student_Info_System.Presentation
{
    public class SchoolPresentation
    {
        private readonly ISchoolService _schoolService;
        private readonly IValidatorService _validatorService;

        public SchoolPresentation(ISchoolService schoolService, IValidatorService validatorService)
        {
            _schoolService = schoolService;
            _validatorService = validatorService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Student Operations");
                Console.WriteLine("2. Department Operations");
                Console.WriteLine("3. Lecture Operations");
                Console.WriteLine("Q. Quit");

                var choice = Console.ReadLine();
                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice.ToUpper())
                {
                    case "1":
                        StudentOperations();
                        break;
                    case "2":
                        DepartmentOperations();
                        break;
                    case "3":
                        LectureOperations();
                        break;
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void StudentOperations()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select a student operation:");
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Transfer Student");
                Console.WriteLine("3. List All Students");
                Console.WriteLine("Q. Back to Main Menu");

                var choice = Console.ReadLine();
                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice.ToUpper())
                {
                    case "1":
                        CreateStudent();
                        break;
                    case "2":
                        TransferStudent();
                        break;
                    case "3":
                        ListAllStudents();
                        break;
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateStudent()
        {
            Console.Clear();
            Console.WriteLine("Enter Student Details:");

            int studentNumber;
            while (true)
            {
                Console.Write("Student Number: ");
                string? studentNumberInput = Console.ReadLine();
                if (int.TryParse(studentNumberInput, out studentNumber))
                {
                    try
                    {
                        _validatorService.ValidateStudentNumber(studentNumber);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the student number.");
                }
            }

            string? firstName;
            while (true)
            {
                Console.Write("First Name: ");
                firstName = Console.ReadLine();
                if (!string.IsNullOrEmpty(firstName))
                {
                    try
                    {
                        _validatorService.ValidateStudentName(firstName);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("First name cannot be empty. Please try again.");
                }
            }

            string? lastName;
            while (true)
            {
                Console.Write("Last Name: ");
                lastName = Console.ReadLine();
                if (!string.IsNullOrEmpty(lastName))
                {
                    try
                    {
                        _validatorService.ValidateStudentName(lastName);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Last name cannot be empty. Please try again.");
                }
            }

            string? email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email))
                {
                    try
                    {
                        _validatorService.ValidateEmail(email);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Email cannot be empty. Please try again.");
                }
            }

            string? departmentCode;
            while (true)
            {
                Console.Write("Department Code: ");
                departmentCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(departmentCode))
                {
                    try
                    {
                        _validatorService.ValidateDepartmentExists(departmentCode);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Department code cannot be empty. Please try again.");
                }
            }

            List<int> lectureIds = new List<int>();
            while (true)
            {
                Console.Write("Lecture IDs (comma-separated): ");
                string? lectureIdsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(lectureIdsInput))
                {
                    var lectureIdsArray = lectureIdsInput.Split(',')
                                                         .Select(id => int.TryParse(id, out var lectureId) ? lectureId : (int?)null)
                                                         .Where(id => id.HasValue)
                                                         .Select(id => id!.Value)
                                                         .ToList();

                    try
                    {
                        foreach (var lectureId in lectureIdsArray)
                        {
                            _validatorService.ValidateLectureExists(lectureId);
                        }
                        lectureIds = lectureIdsArray;
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Lecture IDs cannot be empty. Please try again.");
                }
            }

            try
            {
                var student = new Student
                {
                    StudentNumber = studentNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    DepartmentCode = departmentCode
                };

                _schoolService.CreateStudent(student, departmentCode, lectureIds);
                Console.WriteLine("Student created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        private void TransferStudent()
        {
            Console.Clear();
            Console.WriteLine("Enter Student Number:");

            int studentNumber;
            while (true)
            {
                string? studentNumberInput = Console.ReadLine();
                if (int.TryParse(studentNumberInput, out studentNumber))
                {
                    try
                    {
                        _validatorService.ValidateStudentNumber(studentNumber);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the student number.");
                }
            }

            string? newDepartmentCode;
            while (true)
            {
                Console.WriteLine("Enter New Department Code:");
                newDepartmentCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(newDepartmentCode))
                {
                    try
                    {
                        _validatorService.ValidateDepartmentExists(newDepartmentCode);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Department code cannot be empty. Please try again.");
                }
            }

            List<int> newLectureIds = new List<int>();
            while (true)
            {
                Console.WriteLine("Enter New Lecture IDs (comma separated):");
                string? newLectureIdsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(newLectureIdsInput))
                {
                    var lectureIdsArray = newLectureIdsInput.Split(',')
                                                            .Select(id => int.TryParse(id, out var lectureId) ? lectureId : (int?)null)
                                                            .Where(id => id.HasValue)
                                                            .Select(id => id!.Value)
                                                            .ToList();

                    try
                    {
                        foreach (var lectureId in lectureIdsArray)
                        {
                            _validatorService.ValidateLectureExists(lectureId);
                        }
                        newLectureIds = lectureIdsArray;
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Lecture IDs cannot be empty. Please try again.");
                }
            }

            try
            {
                _schoolService.TransferStudent(studentNumber, newDepartmentCode, newLectureIds);
                Console.WriteLine("Student transferred successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListAllStudents()
        {
            Console.Clear();
            var students = _schoolService.GetAllStudents();
            if (!students.Any())
            {
                Console.WriteLine("No students found.");
            }
            else
            {
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} - {student.StudentNumber}");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void DepartmentOperations()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select a department operation:");
                Console.WriteLine("1. Create Department");
                Console.WriteLine("2. Add Lectures to Department");
                Console.WriteLine("3. Add Students to Department");
                Console.WriteLine("4. List All Departments");
                Console.WriteLine("5. List Lectures by Department");
                Console.WriteLine("6. List Students by Department");
                Console.WriteLine("Q. Back to Main Menu");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateDepartment();
                        break;
                    case "2":
                        AddLecturesToDepartment();
                        break;
                    case "3":
                        AddStudentsToDepartment();
                        break;
                    case "4":
                        ListAllDepartments();
                        break;
                    case "5":
                        ListLecturesByDepartment();
                        break;
                    case "6":
                        ListStudentsByDepartment();
                        break;
                    case "Q":
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateDepartment()
        {
            Console.Clear();
            string? name;
            while (true)
            {
                Console.WriteLine("Enter Department Name:");
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    try
                    {
                        _validatorService.ValidateDepartmentName(name);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Department name cannot be empty. Please try again.");
                }
            }

            string? departmentCode;
            while (true)
            {
                Console.WriteLine("Enter Department Code:");
                departmentCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(departmentCode))
                {
                    try
                    {
                        _validatorService.ValidateDepartmentCode(departmentCode);
                        _validatorService.ValidateUniqueDepartmentCode(departmentCode);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Department code cannot be empty. Please try again.");
                }
            }

            var studentNumbers = new List<int>();
            while (true)
            {
                Console.WriteLine("Enter Student Numbers (comma-separated) or leave empty to skip:");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                var numbers = input.Split(',').Select(n => n.Trim());
                bool valid = true;
                foreach (var number in numbers)
                {
                    if (int.TryParse(number, out int studentNumber))
                    {
                        studentNumbers.Add(studentNumber);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid student number: {number}. Please try again.");
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    break;
                }
            }

            var lectureIds = new List<int>();
            while (true)
            {
                Console.WriteLine("Enter Lecture IDs (comma-separated) or leave empty to skip:");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                var ids = input.Split(',').Select(id => id.Trim());
                bool valid = true;
                foreach (var id in ids)
                {
                    if (int.TryParse(id, out int lectureId))
                    {
                        lectureIds.Add(lectureId);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid lecture ID: {id}. Please try again.");
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    break;
                }
            }

            var department = new Department
            {
                Name = name,
                DepartmentCode = departmentCode
            };

            try
            {
                _schoolService.CreateDepartment(department, studentNumbers, lectureIds);
                Console.WriteLine("Department created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void AddLecturesToDepartment()
        {
            Console.Clear();
            string? departmentCode;
            while (true)
            {
                Console.WriteLine("Enter Department Code:");
                departmentCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(departmentCode))
                {
                    try
                    {
                        _validatorService.ValidateDepartmentExists(departmentCode);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Department code cannot be empty. Please try again.");
                }
            }

            List<int> lectureIds = new List<int>();
            while (true)
            {
                Console.WriteLine("Enter Lecture IDs (comma separated):");
                string? lectureIdsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(lectureIdsInput))
                {
                    var lectureIdsArray = lectureIdsInput.Split(',')
                                                         .Select(id => int.TryParse(id, out var lectureId) ? lectureId : (int?)null)
                                                         .Where(id => id.HasValue)
                                                         .Select(id => id!.Value)
                                                         .ToList();

                    try
                    {
                        foreach (var lectureId in lectureIdsArray)
                        {
                            _validatorService.ValidateLectureExists(lectureId);
                        }
                        lectureIds = lectureIdsArray;
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Lecture IDs cannot be empty. Please try again.");
                }
            }

            try
            {
                _schoolService.AddLecturesToDepartment(departmentCode, lectureIds);
                Console.WriteLine("Lectures added to department successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void AddStudentsToDepartment()
        {
            Console.Clear();
            string? departmentCode;
            while (true)
            {
                Console.WriteLine("Enter Department Code:");
                departmentCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(departmentCode))
                {
                    try
                    {
                        _validatorService.ValidateDepartmentExists(departmentCode);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Department code cannot be empty. Please try again.");
                }
            }

            List<int> studentIds = new List<int>();
            while (true)
            {
                Console.WriteLine("Enter Student IDs (comma separated):");
                string? studentIdsInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(studentIdsInput))
                {
                    var studentIdsArray = studentIdsInput.Split(',')
                                                         .Select(id => int.TryParse(id, out var studentId) ? studentId : (int?)null)
                                                         .Where(id => id.HasValue)
                                                         .Select(id => id!.Value)
                                                         .ToList();

                    try
                    {
                        foreach (var studentId in studentIdsArray)
                        {
                            _validatorService.ValidateStudentNumber(studentId);
                        }
                        studentIds = studentIdsArray;
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Student IDs cannot be empty. Please try again.");
                }
            }

            try
            {
                _schoolService.AddStudentsToDepartment(departmentCode, studentIds);
                Console.WriteLine("Students added to department successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListAllDepartments()
        {
            Console.Clear();
            var departments = _schoolService.GetAllDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.Name} - {department.DepartmentCode}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListLecturesByDepartment()
        {
            Console.Clear();
            Console.WriteLine("Enter Department Code:");
            var departmentCode = Console.ReadLine();

            try
            {
                if (string.IsNullOrEmpty(departmentCode))
                {
                    Console.WriteLine("Invalid department code. Please try again.");
                    return;
                }
                _validatorService.ValidateDepartmentExists(departmentCode);
                var lectures = _schoolService.GetLecturesByDepartment(departmentCode);
                if (!lectures.Any())
                {
                    Console.WriteLine("No lectures found for the given department code.");
                }
                else
                {
                    foreach (var lecture in lectures)
                    {
                        Console.WriteLine($"{lecture.Name} - {lecture.WeekDay} {lecture.StartTime} to {lecture.EndTime}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListStudentsByDepartment()
        {
            Console.Clear();
            Console.WriteLine("Enter Department Code:");
            var departmentCode = Console.ReadLine();
            if (string.IsNullOrEmpty(departmentCode))
            {
                Console.WriteLine("Invalid department code. Please try again.");
                Console.ReadKey();
                return;
            }

            try
            {
                _validatorService.ValidateDepartmentCode(departmentCode);
                _validatorService.ValidateDepartmentExists(departmentCode);

                var students = _schoolService.GetStudentsByDepartment(departmentCode);
                if (!students.Any())
                {
                    Console.WriteLine("No students found for the given department code.");
                }
                else
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName} - {student.StudentNumber}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void LectureOperations()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select a lecture operation:");
                Console.WriteLine("1. Create Lecture");
                Console.WriteLine("2. List All Lectures");
                Console.WriteLine("3. List Lectures by Student");
                Console.WriteLine("Q. Back to Main Menu");

                var choice = Console.ReadLine();
                if (string.IsNullOrEmpty(choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice.ToUpper())
                {
                    case "1":
                        CreateLecture();
                        break;
                    case "2":
                        ListAllLectures();
                        break;
                    case "3":
                        ListLecturesByStudent();
                        break;
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateLecture()
        {
            Console.Clear();
            string? name;
            while (true)
            {
                Console.WriteLine("Enter Lecture Name:");
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    try
                    {
                        _validatorService.ValidateLectureName(name);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Lecture name cannot be empty. Please try again.");
                }
            }

            string weekDay = null;
            while (true)
            {
                Console.WriteLine("Enter lecture weekday (optional):");
                weekDay = Console.ReadLine();

                try
                {
                    weekDay = _validatorService.ValidateWeekDay(weekDay);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            DateTime startTime;
            while (true)
            {
                Console.WriteLine("Enter Lecture Start Time (HH:mm):");
                var input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out startTime))
                {
                    try
                    {
                        _validatorService.ValidateLectureStartTime(startTime);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid time format. Please try again.");
                }
            }

            DateTime endTime;
            while (true)
            {
                Console.WriteLine("Enter Lecture End Time (HH:mm):");
                var input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out endTime))
                {
                    try
                    {
                        _validatorService.ValidateLectureEndTime(endTime);
                        _validatorService.ValidateLectureTime(startTime, endTime);
                        _validatorService.ValidateLectureDuration(startTime, endTime);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid time format. Please try again.");
                }
            }

            string? departmentCode;
            while (true)
            {
                Console.WriteLine("Enter Department Code:");
                departmentCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(departmentCode))
                {
                    try
                    {
                        _validatorService.ValidateDepartmentCode(departmentCode);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Department code cannot be empty. Please try again.");
                }
            }

            var lecture = new Lecture
            {
                Name = name,
                WeekDay = weekDay,
                StartTime = startTime,
                EndTime = endTime
            };

            var existingLectures = _schoolService.GetLecturesByDepartment(departmentCode);
            _validatorService.ValidateLectureOverlap(lecture, existingLectures);

            try
            {
                _schoolService.CreateLecture(lecture, departmentCode);
                Console.WriteLine("Lecture created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListAllLectures()
        {
            Console.Clear();
            var lectures = _schoolService.GetAllLectures();
            foreach (var lecture in lectures)
            {
                Console.WriteLine($"{lecture.Name} - {lecture.WeekDay} {lecture.StartTime} to {lecture.EndTime}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ListLecturesByStudent()
        {
            Console.Clear();
            Console.WriteLine("Enter Student Number:");
            var studentNumberInput = Console.ReadLine();
            if (string.IsNullOrEmpty(studentNumberInput) || !int.TryParse(studentNumberInput, out var studentNumber))
            {
                Console.WriteLine("Invalid student number. Please enter a valid integer.");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
                return;
            }

            try
            {
                Console.WriteLine($"Fetching lectures for student number: {studentNumber}");
                var lectures = _schoolService.GetLecturesByStudent(studentNumber);
                if (lectures == null || !lectures.Any())
                {
                    Console.WriteLine("No lectures found for the given student number.");
                }
                else
                {
                    foreach (var lecture in lectures)
                    {
                        Console.WriteLine($"{lecture.Name} - {lecture.WeekDay} {lecture.StartTime} to {lecture.EndTime}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
